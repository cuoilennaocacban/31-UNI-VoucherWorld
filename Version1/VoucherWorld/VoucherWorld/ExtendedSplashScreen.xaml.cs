using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.GamerServices;
using Newtonsoft.Json.Linq;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using VoucherWorld.Model;
using VoucherWorld.Model.AppLiveInfoModel;
using VoucherWorld.Resources;
using VoucherWorld.Settings;
using VoucherWorld.Utilities;
using VoucherWorld.ViewModel;

namespace VoucherWorld
{
    public partial class ExtendedSplashScreen : PhoneApplicationPage
    {
        public ExtendedSplashScreen()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (NetworkInterface.GetIsNetworkAvailable())
            if (true)
            {
                await LoadData();
            }
            else
            {
                IAsyncResult result = Guide.BeginShowMessageBox(
                    AppResources.SplashScreen_OnNavigatedTo_No_Network,
                    AppResources.SplashScreen_OnNavigatedTo_No_network_details,
                    new string[] {"wifi", "3G/4G-LTE"},
                    0,
                    MessageBoxIcon.Error,
                    null,
                    null
                    );
                result.AsyncWaitHandle.WaitOne();

                int? choice = Guide.EndShowMessageBox(result);
                if (choice.HasValue)
                {
                    if (choice.Value == 0)
                    {
                        ConnectionSettingsTask con = new ConnectionSettingsTask();
                        con.ConnectionSettingsType = ConnectionSettingsType.WiFi;
                        con.Show();
                    }
                    else
                    {
                        ConnectionSettingsTask con = new ConnectionSettingsTask();
                        con.ConnectionSettingsType = ConnectionSettingsType.Cellular;
                        con.Show();
                    }
                }
                else
                {
                    MessageBoxResult res = MessageBox.Show(AppResources.SplashScreen_OnNavigatedTo_OfflineModeQuestion,
                        AppResources.SplashScreen_OnNavigatedTo_Offline_Mode,
                        MessageBoxButton.OKCancel);
                    if (res == MessageBoxResult.Cancel)
                    {
                        StaticMethod.Quit();
                    }
                    else if (res == MessageBoxResult.OK)
                    {
                        StaticData.isOffline = true;
                        //Navigate to MainPage
                        MessageBox.Show("Voucher World required a working internet connection", "We are sorry",
                            MessageBoxButton.OK);
                        StaticMethod.Quit();
                        //NavigationService.Navigate(new Uri("/PageGroups/KaraokeGroup/OfflinePage.xaml", UriKind.Relative));
                    }
                }
            }
            base.OnNavigatedTo(e);
        }

        private async Task LoadData()
        {
            //Cout how many time this app was openned
            CountOpen();
            //Get App's controller parameter
            await CheckParameter();

            //TODO: comment this out when release
            //Common task: Ask if user want to download newer version
            //SetupUI();

            SetupHistory();

            ////Setup the song language
            //SetupLanguage();
            ////Setup the app Word color with painful LongListSelector
            //StaticData.wordColor = ColorSettingsHelper.GetLyricColor();
            ////Setup App's Storage folder
            //SetupStorage();
            ////Setup App's Universal Playlist
            //SetupPlaylist();


            //Navigate to MainPage
            //NavigationService.Navigate(new Uri("/Mainpage.xaml", UriKind.Relative));

            FacebookLoginSettingHelper newHelper = new FacebookLoginSettingHelper();
            if (newHelper.GetLanguage())
            {
                NavigationService.Navigate(new Uri("/PageGroups/LoginGroup/FacebookLoginPage.xaml", UriKind.Relative));
            }
            else
            {
                NavigationService.Navigate(new Uri("/PageGroups/LoginGroup/LoginPage.xaml", UriKind.Relative));
            }
            //Save some performance here
            //progressBar.IsIndeterminate = false;
        }

        private async Task CheckParameter()
        {
            string result = "";

            try
            {
                //result = await StaticMethod.GetHttpAsString("https://dl.dropboxusercontent.com/s/rv74lwons2l4ra8/VoucherWorld.txt");

                result =
                    "{\n  \"AppLiveInfo\": {\n    \"name\": \"Voucher World\",\n    \"ver\": \"1.0.0.0\",\n    \"wp8link\": \"www.google.com.vn\",\n    \"wp7link\": \"www.google.com.vn\",\n    \"w8link\": \"www.google.com.vn\",\n    \"w81link\": \"www.google.com.vn\",\n    \"admode\": \"0\",\n    \"message\": [\n      \"welcome to voucher world first version\",\n      \"hello world\"\n    ]\n  }\n}\n";

                JObject jObject = JObject.Parse(result);

                StaticData.appLiveInfoRootObject = jObject.ToObject<AppLiveInfoRootObject>();

                StaticData.appVersion =
                    System.Reflection.Assembly.GetExecutingAssembly().FullName.Split('=')[1].Split(',')[0];

                if (IsHaveNewerVersion(StaticData.appLiveInfoRootObject.appLiveInfo.ver, StaticData.appVersion))
                {
                    StaticData.EnableAppLink = true;
                }
            }
            catch
            {
            }
        }

        private bool IsHaveNewerVersion(string currentVersion, string appVersion)
        {
            var curVer = new Version(currentVersion);
            var appVer = new Version(appVersion);

            int re = curVer.CompareTo(appVer);

            if (re > 0)
                return true;
            return false;
        }

        //private void SetupLanguage()
        //{
        //    string lan = LanguageSettingHelper.GetLanguage();
        //    if (lan == "en")
        //    {
        //        StaticData.link = "http://star.zing.vn/star/phongthu/Nhac-Anh-My.3.html?alpha=all&sort=lanthu";
        //        StaticData.newLink = "http://star.zing.vn/star/phongthu/Nhac-Anh-My.3.html?alpha=all&sort=time";

        //    }
        //    else
        //    {
        //        StaticData.link = "http://star.zing.vn/star/phongthu/index.html?alpha=all&sort=lanthu";
        //        StaticData.newLink = "http://star.zing.vn/star/phongthu/index.html?alpha=all&sort=time";
        //    }
        //}

        //private void SetupStorage()
        //{
        //    StaticMethod.DeleteFolder(StaticData.musicFolder);
        //    StaticMethod.CreateFolder(StaticData.recordedFolder);
        //    StaticMethod.CreateFolder(StaticData.musicFolder);
        //}

        //public void SetupPlaylist()
        //{
        //    StaticData.playlist.CollectionChanged += playlist_CollectionChanged;
        //}

        private void CountOpen()
        {
            StaticData.openCount = ApplicationSettings.GetSetting<int>("openCount", 0);
            StaticData.openCount++;
            ApplicationSettings.SetSetting<int>("openCount", StaticData.openCount, true);
        }

        private void SetupUI()
        {
            if (StaticData.EnableAppLink)
            {
                if (
                    MessageBox.Show(
                        AppResources.SplashScreen_SetupUI_New_version + StaticData.appLiveInfoRootObject.appLiveInfo.ver,
                        AppResources.SplashScreen_SetupUI_Great_news, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    WebBrowserTask webBrowserTask = new WebBrowserTask();
                    webBrowserTask.Uri = new Uri(StaticData.appLiveInfoRootObject.appLiveInfo.wp8link);
                    webBrowserTask.Show();
                }
            }

            //if (StaticData.playlist.Count > 0)
            //{
            //    ApplicationBarIconButton playlistButton = (ApplicationBarIconButton)this.ApplicationBar.Buttons[0];
            //    playlistButton.IsEnabled = true;
            //}

            if (StaticData.openCount%5 == 0 && ApplicationSettings.GetSetting<bool>("hasReview", false) == false)
            {
                if (MessageBox.Show(AppResources.SplashScreen_SetupUI_RateDetails,
                    AppResources.SplashScreen_SetupUI_Rate,
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    MarketplaceReviewTask marketPlace = new MarketplaceReviewTask();
                    marketPlace.Show();
                    ApplicationSettings.SetSetting<bool>("hasReview", true, true);
                    GoogleAnalytics.EasyTracker.GetTracker().SendSocial("MarketPlace", "rate", "MarketPlace");
                }
                else
                {
                    if (MessageBox.Show(AppResources.SplashScreen_SetupUI_FeedbackDetails,
                        AppResources.SplashScreen_SetupUI_Feedback,
                        MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        EmailComposeTask email = new EmailComposeTask();
                        email.To = "cuoilennaocacban2@hotmail.com";
                        email.Subject = AppResources.SplashScreen_SetupUI__EmailHeader;
                        email.Body = AppResources.SplashScreen_SetupUI_EmailBody;
                        email.Show();

                        //ApplicationSettings.SetSetting<bool>("hasReview", true, true);
                    }
                    else
                    {
                        MessageBox.Show(AppResources.SplashScreen_SetupUI_FeedbackRemind);

                        //No, they didn't
                        //ApplicationSettings.SetSetting<bool>("hasReview", true, true);
                    }
                }
            }
        }

        private void SetupHistory()
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;

            using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //Create a file stream to open or create file
                using (
                    IsolatedStorageFileStream stream = new IsolatedStorageFileStream("EnrollmentHistory.xml",
                        FileMode.OpenOrCreate, isoStorage))
                {
                    XmlSerializer serializer = new XmlSerializer(StaticData.ErrollmentHistory.GetType());
                    //using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                    //{
                    //    // Viết dữ liệu theo Serialize
                    //    serializer.Serialize(xmlWriter, StaticData.ErrollmentHistory);
                    //}


                    try
                    {
                        StaticData.ErrollmentHistory = (ObservableCollection<Enrrollments>) serializer.Deserialize(stream);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        StaticData.ErrollmentHistory = new ObservableCollection<Enrrollments>();
                    }
                }
            }
        }
    }
}