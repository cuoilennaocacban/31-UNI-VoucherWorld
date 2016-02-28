using Facebook;
using Facebook.Client;
using Microsoft.Phone.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using VoucherWorld.Model;
using VoucherWorld.Settings;
using VoucherWorld.Utilities;
using VoucherWorld.ViewModel;

namespace VoucherWorld.PageGroups.LoginGroup
{
    public partial class FacebookLoginPage : PhoneApplicationPage
    {
        public FacebookLoginPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!StaticData.isAuthenticated)
            {
                StaticData.isAuthenticated = true;

                FacebookLoginSettingHelper fb = new FacebookLoginSettingHelper();
                if (fb.GetLanguage())
                {
                    await AutoAuthenticate();
                }
                else
                {
                    await Authenticate();
                }
            }
        }

        private FacebookSession session;
        private async Task Authenticate()
        {
            string message = String.Empty;
            try
            {
                session = await StaticData.FacebookSessionClient.LoginAsync("user_about_me,read_stream,email");
                StaticData.AccessToken = session.AccessToken;
                StaticData.FacebookId = session.FacebookId;

                FacebookClient fb = new FacebookClient(StaticData.AccessToken);
                var temp = await fb.GetTaskAsync("me");
                string jsonTemp = temp.ToString();
                JObject jObject = JObject.Parse(jsonTemp);
                FacebookUser newFacebookUser = jObject.ToObject<FacebookUser>();

                StaticData.CurrentUser.Name = newFacebookUser.name;
                StaticData.CurrentUser.UserName = newFacebookUser.username;
                StaticData.CurrentUser.Password = newFacebookUser.id;
                StaticData.CurrentUser.Email = newFacebookUser.email;
                StaticData.CurrentUser.IsFacebookUser = true;

                StaticMethod.ShowProgress(this, "Registering...", 0, true, true);


                try
                {
                    string tempRegister = await UserAPI.Register(StaticData.CurrentUser);

                    //Save that logged in facebook
                    FacebookLoginSettingHelper newHelper = new FacebookLoginSettingHelper();
                    newHelper.SetLanguage(true);

                    StaticMethod.ShowProgress(this, "Registering...", 0, true, false);
                }
                catch (Exception e)
                {
                    //Username conflict
                    Console.WriteLine(e);
                }

                await Login(StaticData.CurrentUser.UserName, StaticData.CurrentUser.Password);
            }
            catch (InvalidOperationException e)
            {
                message = "Login failed! Exception details: " + e.Message;
                MessageBox.Show(message);
            }
        }

        private async Task AutoAuthenticate()
        {
            string message = String.Empty;
            try
            {
                session = await StaticData.FacebookSessionClient.LoginAsync("user_about_me,read_stream,email");
                StaticData.AccessToken = session.AccessToken;
                StaticData.FacebookId = session.FacebookId;

                FacebookClient fb = new FacebookClient(StaticData.AccessToken);

                var temp = await fb.GetTaskAsync("me");
                string jsonTemp = temp.ToString();
                JObject jObject = JObject.Parse(jsonTemp);
                FacebookUser newFacebookUser = jObject.ToObject<FacebookUser>();

                StaticData.CurrentUser.Name = newFacebookUser.name;
                StaticData.CurrentUser.UserName = newFacebookUser.username;
                StaticData.CurrentUser.Password = newFacebookUser.id;
                StaticData.CurrentUser.Email = newFacebookUser.email;
                StaticData.CurrentUser.IsFacebookUser = true;

                await Login(StaticData.CurrentUser.UserName, StaticData.CurrentUser.Password);

                //Dispatcher.BeginInvoke(
                //    () =>
                //    {
                //        NavigationService.Navigate(new Uri("/PageGroups/MainGroup/MainPage.xaml", UriKind.Relative));

                //    });
            }
            catch (InvalidOperationException e)
            {
                message = "Login failed! Exception details: " + e.Message;
                MessageBox.Show(message);
            }
        }

        private async Task Login(string username, string password)
        {
            StaticMethod.ShowProgress(this, "Logging in...", 0, true, true);
            string temp = await UserAPI.LoginTask(username, password);

            JObject jArray = JObject.Parse(temp);
            StaticData.CurrentUser = jArray.ToObject<User>();

            NavigationService.Navigate(new Uri("/PageGroups/MainGroup/MainPage.xaml", UriKind.Relative));

            StaticMethod.ShowProgress(this, "Logging in...", 0, true, false);
        }
    }
}