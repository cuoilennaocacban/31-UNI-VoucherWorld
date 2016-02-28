using GART.BaseControls;
using GART.Controls;
using GART.Data;
using Microsoft.Phone.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using VoucherWorld.Model;
using VoucherWorld.Utilities;
using VoucherWorld.ViewModel;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace VoucherWorld.PageGroups
{
    public partial class MagicLenPage : PhoneApplicationPage
    {
        public ObservableCollection<Route> RouteCollection { get; set; }
        private ObservableCollection<ARItem> locationsTvrda;
        public MagicLenPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            locationsTvrda = new ObservableCollection<ARItem>();
            overheadMap.Visibility = Visibility.Collapsed;
            worldView.Visibility = Visibility.Visible;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await LoadData();
            InitializeWorld();
        }

        public async Task LoadData()
        {
            string temp = "";

            if (!StaticData.isOfflineMode)
            {
                temp =
                    await
                        StaticMethod.GetHttpAsString(
                            "http://voucherworld.azurewebsites.net/api/routes?lat=10.8221&lon=106.6876&distance=50");
            }
            else
            {
                temp =
                    "[{\"Id\":8,\"Name\":\"VIC Hidden Chocolate\",\"IsHidden\":true,\"Category\":0,\"Distance\":0.085937841775328511,\"Gift\":{\"Id\":8,\"GiftName\":\"Chocolate Ice Cream\",\"Images\":[]},\"PlaceIcon\":\"http://mohinhgiayvn.com/diendan/favicon.ico\",\"Question\":{\"Id\":8,\"Content\":\"Which one do you prefer: in box or on stick?\"},\"Place\":{\"Id\":20,\"Name\":\"Tr\u01b0\u1eddng \u0110\u1ea1i h\u1ecdc C\u00f4ng nghi\u1ec7p, TP.HCM\",\"Address\":\"Tr\u01b0\u1eddng \u0110\u1ea1i h\u1ecdc C\u00f4ng nghi\u1ec7p, TP.HCM\",\"BonusPoint\":0,\"Longitude\":106.6882,\"Latitude\":10.8226,\"Altitude\":0.0,\"Question\":null},\"Merchant\":{\"Id\":4,\"Name\":\"Vi\u1ec7t Ice Cream\",\"Address\":\"Vi\u1ec7t Ice Cream Address\",\"PhoneNumber\":\"0866772508\",\"Website\":\"http://vieticecream.com\"}},{\"Id\":9,\"Name\":\"VIC Another Hidden Chocolate\",\"IsHidden\":true,\"Category\":0,\"Distance\":0.085937841775328511,\"Gift\":{\"Id\":9,\"GiftName\":\"Chocolate Ice Cream\",\"Images\":[]},\"PlaceIcon\":\"http://mohinhgiayvn.com/diendan/favicon.ico\",\"Question\":{\"Id\":9,\"Content\":\"Which flavour do you like most?\"},\"Place\":{\"Id\":20,\"Name\":\"Tr\u01b0\u1eddng \u0110\u1ea1i h\u1ecdc C\u00f4ng nghi\u1ec7p, TP.HCM\",\"Address\":\"Tr\u01b0\u1eddng \u0110\u1ea1i h\u1ecdc C\u00f4ng nghi\u1ec7p, TP.HCM\",\"BonusPoint\":0,\"Longitude\":106.6882,\"Latitude\":10.8226,\"Altitude\":0.0,\"Question\":null},\"Merchant\":{\"Id\":4,\"Name\":\"Vi\u1ec7t Ice Cream\",\"Address\":\"Vi\u1ec7t Ice Cream Address\",\"PhoneNumber\":\"0866772508\",\"Website\":\"http://vieticecream.com\"}}]";
            }

            JArray jArray = JArray.Parse(temp);
            
            RouteCollection = jArray.ToObject<ObservableCollection<Route>>();

            foreach (Enrrollments errollmentse in StaticData.ErrollmentHistory)
            {
                int routeId = errollmentse.Id;
                for (int i = RouteCollection.Count - 1; i >= 0; i--)
                {
                    if (RouteCollection[i].Id == routeId)
                    {
                        RouteCollection.RemoveAt(i);
                    }
                }
            }

            foreach (Route route in RouteCollection)
            {
                if (route.IsHidden)
                {
                    locationsTvrda.Add(route.Place);
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ardisplay.StartServices();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            try
            {
                ardisplay.StopServices();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void InitializeWorld()
        {

            //Add hidden place

            //locationsTvrda.Add(StaticViewModel.MapViewModel.currentPlace);

            //Place testPlace = new Place();
            //testPlace.GeoLocation = new GeoCoordinate(10.833769, 106.681338);
            //testPlace.Latitude = 10.833769;
            //testPlace.Longitude = 106.681338;

            //testPlace.Content = "KFC Diamond 1";
            //testPlace.Address = "Tap here to collect";

            //locationsTvrda.Add(testPlace);

            ardisplay.ARItems = locationsTvrda;
        }

        private void CityItem_OnTap(object sender, GestureEventArgs e)
        {
            Route selectedRoute = sender as Route;
            StaticData.currentRoute = selectedRoute;

            NavigationService.Navigate(
                new Uri("/PageGroups/RouteGroup/RouteDetails.xaml?RouteId=" + selectedRoute.Id, UriKind.Relative));
        }

        private void SwitchHeadingMode()
        {
            if (headingIndicator.RotationSource == RotationSource.AttitudeHeading)
            {
                headingIndicator.RotationSource = RotationSource.North;
                overheadMap.RotationSource = RotationSource.AttitudeHeading;
            }
            else
            {
                overheadMap.RotationSource = RotationSource.North;
                headingIndicator.RotationSource = RotationSource.AttitudeHeading;
            }
        }


        private void MagicLenPage_OnOrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            ControlOrientation orientation = ControlOrientation.Default;

            switch (e.Orientation)
            {
                case PageOrientation.LandscapeLeft:
                    orientation = ControlOrientation.Clockwise270Degrees;
                    break;
                case PageOrientation.LandscapeRight:
                    orientation = ControlOrientation.Clockwise90Degrees;
                    break;
            }

            ardisplay.Orientation = orientation;
        }
    }
}