using System;
using System.Collections.Generic;
using BirthdayApp.Api.Data;
using BirthdayApp.AppCommon;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace BirthdayApp.Api
{
    public class VkService : IVkService
    {
        #region Members

        private readonly VkApi _api;

        #endregion

        #region Props

        public List<FriendViewModel> Friends { get; } = new List<FriendViewModel>();

        public bool IsAuthorized { get; private set; }

        #endregion

        public VkService()
        {
            _api = new VkApi();
        }
        
        public void Authorize(string accessToken)
        {
            try
            {
                _api.Authorize(new ApiAuthParams
                {
                    ApplicationId = AppInfo.VkAppId,
                    AccessToken = accessToken,
                    Settings = Settings.Friends
                });

                IsAuthorized = _api.IsAuthorized;
            }
            catch (Exception e)
            {
                IsAuthorized = false;
            }
            finally
            {
                AuthorizationStatusChanged?.Invoke(this, new AuthorizationStatusChangedEventArgs
                {
                    State = IsAuthorized
                });
            }
        }

        public async void LoadFriends()
        {
            try
            {
                Friends.Clear();

                var friends = await _api.Friends.GetAsync(new FriendsGetParams
                {
                    Fields = ProfileFields.Photo100,
                    Order = FriendsOrder.Hints
                });

                foreach (var friend in friends)
                {
                    Friends.Add(new FriendViewModel(friend));
                }

                FriendsLoaded?.Invoke(this, new FriendsLoadedEventArgs
                {
                    Friends = Friends
                });
            }
            catch
            {
                //ignore
            }
        }

        #region Events

        public event EventHandler<IAuthorizationStatusChangedEventArgs> AuthorizationStatusChanged;
        public event EventHandler<IFriendsLoadedEventArgs> FriendsLoaded;

        #endregion

        private sealed class AuthorizationStatusChangedEventArgs : IAuthorizationStatusChangedEventArgs
        {
            public bool State { get; set; }
        }

        private sealed class FriendsLoadedEventArgs : IFriendsLoadedEventArgs
        {
            public List<FriendViewModel> Friends { get; set; }
        }
    }
}
