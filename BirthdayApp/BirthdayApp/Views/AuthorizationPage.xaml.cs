using BirthdayApp.ViewModels;
using Xamarin.Forms;

namespace BirthdayApp.Views
{
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            
            WebView.Navigated += WebView_OnNavigated;
        }

        private void WebView_OnNavigated(object sender, WebNavigatedEventArgs webNavigatedEventArgs)
        {
            var viewModel = GetViewModel();
            viewModel.ParseRedirectUrl(webNavigatedEventArgs.Url);
        }

        private AuthorizationPageViewModel GetViewModel()
        {
            return (AuthorizationPageViewModel) BindingContext;
        }
    }
}
