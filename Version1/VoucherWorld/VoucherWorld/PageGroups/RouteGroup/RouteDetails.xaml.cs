using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Device.Location;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using VoucherWorld.CustomControl;
using VoucherWorld.Model;
using VoucherWorld.Utilities;
using VoucherWorld.ViewModel;

namespace VoucherWorld.PageGroups.RouteGroup
{
    public partial class RouteDetails : PhoneApplicationPage
    {

        //Sample Data
        //Start Point of Route: 10.826718, 106.679503

        private string routeId;
        private string temp;
        private bool isLoaded = false;

        public RouteDetails()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            

            if (NavigationContext.QueryString.TryGetValue("RouteId", out routeId))
            {
                string name = routeId;
            }

            if (e.NavigationMode != NavigationMode.Back)
            {
                this.Loaded += OnLoaded;
            }
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!isLoaded)
            {
                if (!StaticData.isHistory)
                {
                    await GetRouteDetails(routeId);
                }
                else
                {
                    await GetHistoryRouteDetails(routeId);
                }
                isLoaded = true;
            }
        }

        private async Task GetRouteDetails(string Id)
        {
            string temp = "";

            if (!StaticData.isOfflineMode)
            {
                temp =
                    await StaticMethod.GetHttpAsString("http://voucherworld.azurewebsites.net/api/routes?id=" + Id);
            }
            else
            {
                temp =
                    "{\"Id\":10,\"Name\":\"Vi\u1ec7t Ice Cream Nguy\u1ec5n V\u0103n Nghi\",\"IsHidden\":false,\"Category\":0,\"Gift\":{\"Id\":10,\"GiftName\":\"Chocolate Ice Cream\",\"Images\":[]},\"PlaceIcon\":null,\"Question\":{\"Id\":10,\"Content\":\"Which flavour do you like most\"},\"Places\":[{\"Id\":19,\"Name\":\"47 Nguy\u1ec5n V\u0103n Nghi, P4, G\u00f2 V\u1ea5p\",\"Address\":\"47 Nguy\u1ec5n V\u0103n Nghi, P4, G\u00f2 V\u1ea5p\",\"BonusPoint\":5,\"Longitude\":106.6872,\"Latitude\":10.82354,\"Altitude\":0.0,\"Question\":{\"Id\":19,\"Content\":\"Are Vi\u1ec7t Ice Cream\'s employees friendly?\"}},{\"Id\":20,\"Name\":\"Tr\u01b0\u1eddng \u0110\u1ea1i h\u1ecdc C\u00f4ng nghi\u1ec7p, TP.HCM\",\"Address\":\"Tr\u01b0\u1eddng \u0110\u1ea1i h\u1ecdc C\u00f4ng nghi\u1ec7p, TP.HCM\",\"BonusPoint\":5,\"Longitude\":106.6882,\"Latitude\":10.8226,\"Altitude\":0.0,\"Question\":{\"Id\":20,\"Content\":\"How does Milk-Coconut flavour satisfy you?\"}},{\"Id\":21,\"Name\":\"Kem Vi\u1ec7t Nguy\u1ec5n V\u0103n Nghi\",\"Address\":\"Kem Vi\u1ec7t Nguy\u1ec5n V\u0103n Nghi\",\"BonusPoint\":5,\"Longitude\":106.6885,\"Latitude\":10.8218,\"Altitude\":0.0,\"Question\":{\"Id\":21,\"Content\":\"Is Vi\u1ec7t Ice Cream\'s website friendly?\"}}],\"Merchant\":{\"Id\":4,\"Name\":\"Vi\u1ec7t Ice Cream\",\"Address\":\"Vi\u1ec7t Ice Cream Address\",\"PhoneNumber\":\"0866772508\",\"Website\":\"http://vieticecream.com\"}}";
            }

            //string temp =
            //    "{\"RouteId\":1,\"Name\":\"KFC\",\"Category\":0,\"Gifts\":[{\"GiftId\":1,\"GiftName\":\"Name of Gift_0\",\"InfoImages\":[],\"RouteId\":1}],\"Places\":[{\"PlaceId\":1,\"Longitude\":106.697502,\"Latitude\":10.781637,\"Altitude\":null,\"Address\":\"ĐƯỜNG Phạm Ngọc Thạch 4, Ho Chi Minh City, Vietnam\",\"PlaceType\":0},{\"PlaceId\":2,\"Longitude\":106.697715,\"Latitude\":10.78103,\"Altitude\":null,\"Address\":\"ĐƯỜNG Phạm Ngọc Thạch 1, Ho Chi Minh City, Vietnam\",\"PlaceType\":1},{\"PlaceId\":3,\"Longitude\":106.698342,\"Latitude\":10.781088,\"Altitude\":null,\"Address\":\"Diamond Plaza, tầng 5\",\"PlaceType\":2}]}";

            JObject jObject = JObject.Parse(temp);

            StaticViewModel.MapViewModel.RouteDetailsModel = jObject.ToObject<RouteDetailsModel>();

            StaticData.CurrentErrollments = jObject.ToObject<Enrrollments>();

            InitializeUI();
        }

        private async Task GetHistoryRouteDetails(string Id)
        {

            string temp = "";

            if (!StaticData.isOfflineMode)
            {
                temp =
                    await StaticMethod.GetHttpAsString("http://voucherworld.azurewebsites.net/api/routes?id=" + Id);
            }
            else
            {
                temp =
                    "{\"Id\":10,\"Name\":\"Vi\u1ec7t Ice Cream Nguy\u1ec5n V\u0103n Nghi\",\"IsHidden\":false,\"Category\":0,\"Gift\":{\"Id\":10,\"GiftName\":\"Chocolate Ice Cream\",\"Images\":[]},\"PlaceIcon\":null,\"Question\":{\"Id\":10,\"Content\":\"Which flavour do you like most\"},\"Places\":[{\"Id\":19,\"Name\":\"47 Nguy\u1ec5n V\u0103n Nghi, P4, G\u00f2 V\u1ea5p\",\"Address\":\"47 Nguy\u1ec5n V\u0103n Nghi, P4, G\u00f2 V\u1ea5p\",\"BonusPoint\":5,\"Longitude\":106.6872,\"Latitude\":10.82354,\"Altitude\":0.0,\"Question\":{\"Id\":19,\"Content\":\"Are Vi\u1ec7t Ice Cream\'s employees friendly?\"}},{\"Id\":20,\"Name\":\"Tr\u01b0\u1eddng \u0110\u1ea1i h\u1ecdc C\u00f4ng nghi\u1ec7p, TP.HCM\",\"Address\":\"Tr\u01b0\u1eddng \u0110\u1ea1i h\u1ecdc C\u00f4ng nghi\u1ec7p, TP.HCM\",\"BonusPoint\":5,\"Longitude\":106.6882,\"Latitude\":10.8226,\"Altitude\":0.0,\"Question\":{\"Id\":20,\"Content\":\"How does Milk-Coconut flavour satisfy you?\"}},{\"Id\":21,\"Name\":\"Kem Vi\u1ec7t Nguy\u1ec5n V\u0103n Nghi\",\"Address\":\"Kem Vi\u1ec7t Nguy\u1ec5n V\u0103n Nghi\",\"BonusPoint\":5,\"Longitude\":106.6885,\"Latitude\":10.8218,\"Altitude\":0.0,\"Question\":{\"Id\":21,\"Content\":\"Is Vi\u1ec7t Ice Cream\'s website friendly?\"}}],\"Merchant\":{\"Id\":4,\"Name\":\"Vi\u1ec7t Ice Cream\",\"Address\":\"Vi\u1ec7t Ice Cream Address\",\"PhoneNumber\":\"0866772508\",\"Website\":\"http://vieticecream.com\"}}";
            }

            JObject jObject = JObject.Parse(temp);

            StaticViewModel.MapViewModel.RouteDetailsModel = jObject.ToObject<RouteDetailsModel>();

            InitializeUI();
        }

        private void InitializeUI()
        {
            int i = StaticData.CurrentErrollments.CompletedPlace;
            RouteNameTextBlock.Text = StaticViewModel.MapViewModel.RouteDetailsModel.Name;
            AddressTextBlock.Text = StaticViewModel.MapViewModel.RouteDetailsModel.Places[i].Address;
            CreatorTextBlock.Text = StaticViewModel.MapViewModel.RouteDetailsModel.Merchant.Name;
            //AddressRun.Text = "Address: " + StaticViewModel.MapViewModel.RouteDetailsModel.Places[i].Address;
            //CategoryRun.Text = StaticViewModel.MapViewModel.RouteDetailsModel.Category.ToString();
            //CategoryRun.Text = "Catergory: Food";

            GeoCoordinate fisrtPointCoordinate =
                new GeoCoordinate(StaticViewModel.MapViewModel.RouteDetailsModel.Places[i].Latitude,
                    StaticViewModel.MapViewModel.RouteDetailsModel.Places[i].Longitude);
            MainMap.Center = fisrtPointCoordinate;
            MainMap.ZoomLevel = 16;

            DiamondControl newDiamondControl = new DiamondControl();
            MapOverlay diamondOverlay = new MapOverlay();
            diamondOverlay.Content = newDiamondControl;

            diamondOverlay.GeoCoordinate = fisrtPointCoordinate;

            MapLayer tempMapLayer = new MapLayer();
            tempMapLayer.Add(diamondOverlay);

            MainMap.Layers.Add(tempMapLayer);
        }

        private void PlaylistButton_OnClick(object sender, EventArgs e)
        {
            if (StaticData.isHistory)
            {
                int nextPlaceIndex = StaticData.CurrentErrollments.CompletedPlace;
                if (nextPlaceIndex == 2)
                {
                    NavigationService.Navigate(new Uri("/PageGroups/QuestionGroup/GiftCodePage.xaml", UriKind.Relative));
                }
                else
                {
                    NavigationService.Navigate(new Uri("/PageGroups/MapGroup/NavigationMapPage.xaml?RouteId=" + routeId, UriKind.Relative));
                }
            }
            else
            {
                NavigationService.Navigate(new Uri("/PageGroups/MapGroup/NavigationMapPage.xaml?RouteId=" + routeId, UriKind.Relative));
            }
            
        }
    }
}