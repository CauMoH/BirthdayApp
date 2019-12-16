using System;
using System.Threading.Tasks;
using Prism.Mvvm;
using VkNet.Model;
using Xamarin.Forms;

namespace BirthdayApp.Api.Data
{
    public class FriendViewModel : BindableBase
    {
        #region Members

        private readonly User _user;

        private ImageSource _photo;

        #endregion

        #region Props

        /// <summary>
        /// Id пользователя
        /// </summary>
        public long Id => _user.Id;

        /// <summary>
        /// Полное имя пользователя
        /// </summary>
        public string FullName => string.Join(" ", _user.FirstName, _user.LastName);

        /// <summary>
        /// Фото
        /// </summary>
        public ImageSource Photo
        {
            get => _photo;
            set => SetProperty(ref _photo, value);
        }

        #endregion

        public FriendViewModel(User user)
        {
            _user = user;

            LoadPhoto();
        }

        private async void LoadPhoto()
        {
            if (_user.Photo100 == null)
                return;

            var url = _user.Photo100.AbsoluteUri;
            if (url.Contains("?ava=1"))
            {
                url = url.Replace("?ava=1", "");
            }

            System.Uri.TryCreate(url, UriKind.Absolute, out var uri);
            Photo = await Task<ImageSource>.Factory.StartNew(() => ImageSource.FromUri(uri));
        }
    }
}
