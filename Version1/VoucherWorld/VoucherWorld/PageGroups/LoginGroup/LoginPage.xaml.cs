using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using Newtonsoft.Json.Linq;
using VoucherWorld.Model;
using VoucherWorld.Settings;
using VoucherWorld.Utilities;
using VoucherWorld.ViewModel;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace VoucherWorld.PageGroups.LoginGroup
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            CloudFlyStoryBoard.Begin();
            Cloud2FlyStoryBoard.Begin();
            FacebookLoginSettingHelper fb = new FacebookLoginSettingHelper();
            if (fb.GetLanguage())
            {
                NavigationService.Navigate(new Uri("/PageGroups/LoginGroup/FacebookLoginPage.xaml", UriKind.Relative));
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavigationService.RemoveBackEntry();

            string username, password = "";
            if (NavigationContext.QueryString.TryGetValue("username", out username))
            {
                NavigationContext.QueryString.TryGetValue("password", out password);
                string u = username;
                string p = password;
                await AutoLogin(u, p);
            }
        }

        private async Task AutoLogin(string username, string pasword)
        {
            UserNameTextBox.Text = username;
            PasswordTextBox.Password = pasword;

            await Login(username, pasword);
        }

        private async Task Login(string username, string password)
        {
            StaticMethod.ShowProgress(this, "Logging in...", 0, true, true);

            string temp;

            if (!StaticData.isOfflineMode)
            {
                temp = await UserAPI.LoginTask(username, password);
            }
            else
            {
                temp =
                    "{\"IsFacebookUser\":false,\"Point\":0,\"Enrollments\":[],\"Answers\":[],\"Id\":8,\"Name\":\"Tu\u1ea5n Tr\u1ea7n V\u0103n\",\"UserName\":\"tuantv\",\"Password\":\"123123\",\"UserType\":3,\"Email\":\"cuoilennaocacban@gmail.com\",\"Address\":\"Th\u1ee7 \u0110\u1ee9c, HCMC\",\"PhoneNumber\":\"0866771508\",\"RegistrationDate\":\"2014-03-27T21:52:16.737Z\",\"ObjectState\":0}";
            }

            JObject jArray = JObject.Parse(temp);
            StaticData.CurrentUser = jArray.ToObject<User>();

            NavigationService.Navigate(new Uri("/PageGroups/MainGroup/MainPage.xaml", UriKind.Relative));

            StaticMethod.ShowProgress(this, "Logging in...", 0, true, false);
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            if (MessageBox.Show("Are you sure to exit Voucher World?", "Close app confirmation",
                MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                StaticMethod.Quit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void FacebookLoginButton_OnTap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageGroups/LoginGroup/FacebookLoginPage.xaml", UriKind.Relative));
        }

        private async void LoginButton_OnTap(object sender, GestureEventArgs e)
        {
            await Login(UserNameTextBox.Text, PasswordTextBox.Password);
        }

        private void RegisterHyperlinkButton_OnTap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageGroups/LoginGroup/RegisterPage.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Toggle Offline mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleCloudIcon_OnTap(object sender, GestureEventArgs e)
        {
            if (!StaticData.isOfflineMode)
            {
                StaticMethod.ShowProgress(this, "Offline mode ON", 2000, false, true);
                StaticData.isOfflineMode = true;
            }
            else
            {
                StaticMethod.ShowProgress(this, "Offline mode OFF", 2000, false, true);
                StaticData.isOfflineMode = false;
            }
        }

        private async void DoubleCloudIcon_OnTap(object sender, GestureEventArgs e)
        {
            if (MessageBox.Show("Reset?", "RESET", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                foreach (Enrrollments enrrollments in StaticData.ErrollmentHistory)
                {
                    try
                    {
                        await
                            StaticMethod.GetHttpAsString(
                                "http://voucherworld.azurewebsites.net/api/routes/cancel?userId=11&routeId=" +
                                enrrollments.Id);
                    }
                    catch(Exception ex)
                    {}
                }

                ResetHistory();
            }
        }

        private void ResetHistory()
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;

            using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //Create a file stream to open or create file
                using (
                    IsolatedStorageFileStream stream = new IsolatedStorageFileStream("EnrollmentHistory.xml",
                        FileMode.Create, isoStorage))
                {
                    StaticData.ErrollmentHistory = new ObservableCollection<Enrrollments>();
                }
            }
        }
    }
}