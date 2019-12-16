using System;
using System.Web;
using BirthdayApp.Views;
using Prism.Navigation;

namespace BirthdayApp.ViewModels
{
	public class AuthorizationPageViewModel : ViewModelBase
	{
	    #region Members

	    private readonly INavigationService _navigationService;
        private string _url;

        #endregion

	    #region Props

	    public string Url
	    {
	        get => _url;
	        set => SetProperty(ref _url, value);
	    }

	    #endregion

        public AuthorizationPageViewModel(INavigationService navigationService)
	        : base(navigationService)
        {
            _navigationService = navigationService;

            Title = Localization.strings.AuthorizationPageTitle;

            Url = Api.ApiUrls.OauthUrl;
        }

	    public override void OnNavigatedTo(INavigationParameters parameters)
	    {
	        Url = Api.ApiUrls.OauthUrl;
	    }
        
        public void ParseRedirectUrl(string url)
	    {
	        if (url.Contains("access_token"))
	        {
                var uri = new Uri(url.Replace("#", "?"));

                var accessToken = HttpUtility.ParseQueryString(uri.Query).Get("access_token");
                var userId = HttpUtility.ParseQueryString(uri.Query).Get("user_id");

                var navigateParams = new NavigationParameters
                {
                    {"accessToken", accessToken},
                    {"userId", userId}
                };

	            _navigationService.NavigateAsync(nameof(MasterMainPage) + "/" + nameof(MainPage), navigateParams);
            }
            else
            {
                Url = Api.ApiUrls.OauthUrl;
            }
        }
	}
}
