using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using VoucherWorld.ViewModel;

namespace VoucherWorld.PageGroups.UserInfoGroup
{
    public partial class UserInfoPage : PhoneApplicationPage
    {
        public UserInfoPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.DataContext = StaticData.CurrentUser;

            LoadPicture();
        }

        private void LoadPicture()
        {
            if (StaticData.CurrentUser.IsFacebookUser)
            {
                var profilePictureUrl = string.Format(
                    "https://graph.facebook.com/{0}/picture?type={1}&access_token={2}",
                    StaticData.FacebookId, "square", StaticData.AccessToken);
                AvatarPath.Visibility = Visibility.Collapsed;
                AvatarImage.Source = new BitmapImage(new Uri(profilePictureUrl));
            }
        }
    }
}