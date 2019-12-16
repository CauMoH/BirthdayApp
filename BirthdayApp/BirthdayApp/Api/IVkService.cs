using System;
using System.Collections.Generic;
using BirthdayApp.Api.Data;

namespace BirthdayApp.Api
{
    public interface IVkService
    {
        List<FriendViewModel> Friends { get; }
        bool IsAuthorized { get; }

        void Authorize(string accessToken);
        void LoadFriends();

        event EventHandler<IAuthorizationStatusChangedEventArgs> AuthorizationStatusChanged;
        event EventHandler<IFriendsLoadedEventArgs> FriendsLoaded;
    }

    public interface IAuthorizationStatusChangedEventArgs
    {
        bool State { get; }
    }

    public interface IFriendsLoadedEventArgs
    {
        List<FriendViewModel> Friends { get; }
    }

}
