using GART;
using GART.BaseControls;
using GART.Controls;
using GART.Data;
using Microsoft.Phone.Controls;
using System;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Windows;
using System.Windows.Navigation;
using VoucherWorld.Model;
using VoucherWorld.ViewModel;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace VoucherWorld.PageGroups.MapGroup
{
    public partial class CollectPage : PhoneApplicationPage
    {
        private ObservableCollection<ARItem> locationsTvrda;

        public CollectPage()
        {
            InitializeComponent();
            locationsTvrda = new ObservableCollection<ARItem>();
            overheadMap.Visibility = Visibility.Collapsed;
            worldView.Visibility = Visibility.Visible;
            InitializeWorld();
            
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
            locationsTvrda = new ObservableCollection<ARItem>();
            StaticViewModel.MapViewModel.currentPlace.GeoLocation =
                new GeoCoordinate(StaticViewModel.MapViewModel.currentPlace.Latitude,
                    StaticViewModel.MapViewModel.currentPlace.Longitude);
            StaticViewModel.MapViewModel.currentPlace.Content = StaticViewModel.MapViewModel.currentPlace.Address;

            locationsTvrda.Add(StaticViewModel.MapViewModel.currentPlace);

            Place testPlace = new Place();
            testPlace.GeoLocation = new GeoCoordinate(10.833769, 106.681338);
            testPlace.Latitude = 10.833769;
            testPlace.Longitude = 106.681338;

            testPlace.Content = "KFC Diamond 1";
            testPlace.Address = "Tap here to collect";

            locationsTvrda.Add(testPlace);

            ardisplay.ARItems = locationsTvrda;
        }


        private void CityItem_OnTap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageGroups/QuestionGroup/QuestionPage.xaml", UriKind.Relative));
        }

        private void ThreeDButton_OnClick(object sender, EventArgs e)
        {
            UIHelper.ToggleVisibility(worldView);
        }

        private void HeadingButton_OnClick(object sender, EventArgs e)
        {
            //UIHelper.ToggleVisibility(headingIndicator);
            NavigationService.Navigate(new Uri("/PageGroups/QuestionGroup/QuestionPage.xaml", UriKind.Relative));
        }

        private void MapButton_OnClick(object sender, EventArgs e)
        {
            UIHelper.ToggleVisibility(overheadMap);
        }

        private void RotateDButton_OnClick(object sender, EventArgs e)
        {
            SwitchHeadingMode();
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

        private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            base.OnOrientationChanged(e);

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