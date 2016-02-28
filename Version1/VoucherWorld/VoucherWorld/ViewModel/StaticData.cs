using System;
using System.Collections.ObjectModel;
using Facebook.Client;
using Microsoft.WindowsAzure.MobileServices;
using VoucherWorld.Model;
using VoucherWorld.Model.AppLiveInfoModel;

namespace VoucherWorld.ViewModel
{
    public class StaticData
    {
        public static MobileServiceClient MobileService =
            new MobileServiceClient("https://voucherworld.azure-mobile.net/", "yMkwgliGFHLxEELUoAEDCLrTEtbLGS86");

        public static string appVersion;
        public static bool EnableAppLink = false;

        public static AppLiveInfoRootObject appLiveInfoRootObject;

        //Old code
        public static bool isOffline = false;

        public static int openCount = 0;

        #region For facebook login
        public static readonly string FacebookAppId = "1390189364586853";
        internal static string AccessToken = String.Empty;
        internal static string FacebookId = String.Empty;
        public static bool isAuthenticated = false;
        public static FacebookSessionClient FacebookSessionClient = new FacebookSessionClient(FacebookAppId);
        #endregion

        public static User CurrentUser = new User();

        public static ObservableCollection<Enrrollments> ErrollmentHistory = new ObservableCollection<Enrrollments>();

        public static Enrrollments CurrentErrollments = new Enrrollments();
        public static Route currentRoute;
        public static bool isHistory = false;

        public static bool isOfflineMode = false;
    }

    public class StaticViewModel
    {
        public static RouteListViewModel RouteListViewModel = new RouteListViewModel();

        public static MapViewModel MapViewModel = new MapViewModel();
    }
}
