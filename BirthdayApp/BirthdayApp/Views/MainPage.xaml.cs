using System;
using System.ComponentModel;
using BirthdayApp.ViewModels;
using Xamarin.Forms;

namespace BirthdayApp.Views
{
    public partial class MainPage
    {
        #region Members

        private MainPageViewModel _viewModel;

        #endregion

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnBindingContextChanged(object sender, EventArgs eventArgs)
        {
            _viewModel = (MainPageViewModel)BindingContext;
            _viewModel.PropertyChanged += ViewModel_OnPropertyChanged;

            UpdateAuthStatusBox();
        }

        private void ViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.IsAuthorized))
            {
                UpdateAuthStatusBox();
            }
        }

        private void UpdateAuthStatusBox()
        {
            AuthStatusBox.Color = _viewModel.IsAuthorized ? Color.Green : Color.Red;
        }
    }
}