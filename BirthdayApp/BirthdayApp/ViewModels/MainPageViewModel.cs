using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BirthdayApp.Api;
using BirthdayApp.Api.Data;
using DryIoc;
using Prism.Commands;
using Prism.Navigation;

namespace BirthdayApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Members

        private readonly INavigationService _navigationService;
        private readonly IVkService _vkService;
        
        private bool _isAuthorized;
        #endregion

        #region Props

        public bool IsAuthorized
        {
            get => _isAuthorized;
            set => SetProperty(ref _isAuthorized, value);
        }

        public ObservableCollection<FriendViewModel> Friends { get; } = new ObservableCollection<FriendViewModel>();

        #endregion

        public MainPageViewModel(INavigationService navigationService, IVkService vkService) : base(navigationService)
        {
            _navigationService = navigationService;
            _vkService = vkService;

            _vkService.AuthorizationStatusChanged += VkService_OnAuthorizationStatusChanged;
            _vkService.FriendsLoaded += VkService_OnFriendsLoaded;

            Title = Localization.strings.MainPageTitle;
            InitCommands();

            Load();
        }

        private void Load()
        {
            IsAuthorized = _vkService.IsAuthorized;
            LoadFriends(_vkService.Friends);
        }

        private void LoadFriends(IEnumerable<FriendViewModel> friends)
        {
            Friends.Clear();

            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }

        #region Event Handlers

        private void VkService_OnFriendsLoaded(object sender, IFriendsLoadedEventArgs e)
        {
            LoadFriends(e.Friends);
        }

        private void VkService_OnAuthorizationStatusChanged(object sender, IAuthorizationStatusChangedEventArgs e)
        {
            IsAuthorized = e.State;
        }

        #endregion

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("accessToken"))
            {
                var accessToken = parameters["accessToken"] as string;
                var userId = parameters["userId"] as string;

                if(string.IsNullOrWhiteSpace(accessToken))
                    return;

                _vkService.Authorize(accessToken);
            }
        }

        public override void Destroy()
        {
            _vkService.AuthorizationStatusChanged -= VkService_OnAuthorizationStatusChanged;
            _vkService.FriendsLoaded -= VkService_OnFriendsLoaded;

            base.Destroy();
        }

        #region Command

        private void InitCommands()
        {
            LoadFriendsCommand = new DelegateCommand(LoadFriendsExecute);
        }

        #region Props

        public ICommand LoadFriendsCommand { get; private set; }

        #endregion

        private void LoadFriendsExecute()
        {
            _vkService.LoadFriends();
        }

        #endregion
    }
}
