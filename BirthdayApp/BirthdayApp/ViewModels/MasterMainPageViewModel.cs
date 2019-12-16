using System.Windows.Input;
using BirthdayApp.Views;
using Prism.Commands;
using Prism.Navigation;

namespace BirthdayApp.ViewModels
{
	public class MasterMainPageViewModel : ViewModelBase
	{
        #region Members

	    private readonly INavigationService _navigationService;

        #endregion

        public MasterMainPageViewModel(INavigationService navigationService)
	    : base(navigationService)
        {
            _navigationService = navigationService;

            InitCommands();
        }

	    #region Command

	    private void InitCommands()
	    {
	        NavigateToAuthorizationPageCommand = new DelegateCommand(NavigateToAuthorizationPageExecute);
            NavigateToMainPageCommand = new DelegateCommand(NavigateToMainPageExecute);
	    }

        #region Props

	    public ICommand NavigateToAuthorizationPageCommand { get; private set; }
	    public ICommand NavigateToMainPageCommand { get; private set; }

        #endregion
        
        private void NavigateToAuthorizationPageExecute()
	    {
	        _navigationService.NavigateAsync(nameof(MasterMainPage) + "/" + nameof(AuthorizationPage));
	    }

	    private void NavigateToMainPageExecute()
	    {
	        _navigationService.NavigateAsync(nameof(MasterMainPage) + "/" + nameof(MainPage));
        }

        #endregion
    }
}
