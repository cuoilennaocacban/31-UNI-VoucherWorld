using Microsoft.Devices.Sensors;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Serialization;
using VoucherWorld.CustomControl;
using VoucherWorld.Model;
using VoucherWorld.Utilities;
using VoucherWorld.ViewModel;
using Windows.Devices.Geolocation;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace VoucherWorld.PageGroups.MapGroup
{
    public partial class NavigationMapPage : PhoneApplicationPage
    {
        private Compass compass;
        private DispatcherTimer timer;

        private double magneticHeading;
        private double trueHeading;
        private double headingAccuracy;
        private Vector3 rawMagnetometerReading;
        private bool isDataValid;

        private bool calibrating = false;

        private Geolocator geolocator = null;
        private GeoCoordinate currCoordinate = new GeoCoordinate();

        private List<Place> places = new List<Place>(); 

        private string routeId;

        MapLayer PlacesMapLayer = new MapLayer();
        MapOverlay diamondOverlay = new MapOverlay();

        public NavigationMapPage()
        {
            InitializeComponent();
            //this.Loaded += OnLoaded;
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
                //this.Loaded += OnLoaded;
                Init();
                MainMap.Layers.Add(PlacesMapLayer);
                places = StaticViewModel.MapViewModel.RouteDetailsModel.Places;
                StartPlay();
                if (StaticData.isHistory)
                {
                    int nextPlaceIndex = StaticData.CurrentErrollments.CompletedPlace;
                    if (nextPlaceIndex < 2)
                    {
                        //nextPlaceIndex++;
                        DiamondNumberTextBlock.Text = nextPlaceIndex + "/3";
                        
                    }
                    else
                    {
                        
                    }
                }
            }

            if (e.NavigationMode == NavigationMode.Back)
            {
                //places = StaticViewModel.MapViewModel.RouteDetailsModel.Places;
                int nextPlaceIndex = places.IndexOf(StaticViewModel.MapViewModel.currentPlace);
                if (nextPlaceIndex < 2)
                {
                    nextPlaceIndex++;
                    DiamondNumberTextBlock.Text = nextPlaceIndex + "/3";
                    StaticData.CurrentUser.Point = StaticData.CurrentUser.Point + 10;
                    PointTextBlock.Text = StaticData.CurrentUser.Point.ToString();
                }

                StaticViewModel.MapViewModel.currentPlace = places[nextPlaceIndex];
                StaticData.CurrentErrollments.CompletedPlace = nextPlaceIndex;

                ResetDiamond();
            }
        }

        private async void StartPlay()
        {
            if (!StaticData.isHistory)
            {
                string temp = "";

                if (!StaticData.isOfflineMode)
                {
                    temp = await StaticMethod.GetHttpAsString(
                        "http://voucherworld.azurewebsites.net/api/routes/start?routeId=" + routeId + "&userId=" +
                        StaticData.CurrentUser.Id);
                }
                else
                {
                    temp = "y5eaimzjg";
                }

                StaticData.CurrentErrollments.GiftCode = temp;

                StaticData.ErrollmentHistory.Add(StaticData.CurrentErrollments);
                StaticViewModel.RouteListViewModel.RouteCollection.Remove(StaticData.currentRoute);

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
                        using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                        {
                            // Viết dữ liệu theo Serialize
                            serializer.Serialize(xmlWriter, StaticData.ErrollmentHistory);
                        }
                    }
                }
            }

            //string temp = "{\"GiftCodeId\":2,\"Content\":\"gyuyrcvsh\",\"NormalUserId\":14,\"GiftId\":1}";
            InitializeDiamond();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            GeoCoordinate fisrtPointCoordinate = new GeoCoordinate(10.826718, 106.679503);
            double ZoomLevel = 16;

            ZoomToLocation(fisrtPointCoordinate, ZoomLevel);

            InitializeCompass();
            InitializeLocation();
        }

        private void Init()
        {
            GeoCoordinate fisrtPointCoordinate = new GeoCoordinate(10.826718, 106.679503);
            double ZoomLevel = 16;
            
            ZoomToLocation(fisrtPointCoordinate, ZoomLevel);

            InitializeCompass();
            InitializeLocation();
        }

        //TODO: Initialize Diamond Pushpin
        private void InitializeDiamond()
        {
            int i = StaticData.CurrentErrollments.CompletedPlace;

            DiamondControl newDiamondControl = new DiamondControl();
            newDiamondControl.Tag = places[i];

            StaticViewModel.MapViewModel.currentPlace = places[i];

            diamondOverlay.Content = newDiamondControl;
            diamondOverlay.GeoCoordinate = new GeoCoordinate(places[i].Latitude, places[i].Longitude);
            
            PlacesMapLayer.Clear();

            PlacesMapLayer.Add(diamondOverlay);

            //Test place

            Place testPlace = new Place();
            testPlace.GeoLocation = new GeoCoordinate(10.833769, 106.681338);
            testPlace.Latitude = 10.833769;
            testPlace.Longitude = 106.681338;

            testPlace.Content = "Test place";

            DiamondControl testDiamondControl = new DiamondControl();
            testDiamondControl.Tag = testPlace;

            MapOverlay testOverlay = new MapOverlay();
            testOverlay.Content = testDiamondControl;
            testOverlay.GeoCoordinate = testPlace.GeoLocation;

            PlacesMapLayer.Add(testOverlay);
        }

        private void ResetDiamond()
        {
            DiamondControl newDiamondControl = new DiamondControl();
            newDiamondControl.Tag = StaticViewModel.MapViewModel.currentPlace;

            diamondOverlay = new MapOverlay();
            diamondOverlay.Content = newDiamondControl;
            diamondOverlay.GeoCoordinate = new GeoCoordinate(StaticViewModel.MapViewModel.currentPlace.Latitude,
                StaticViewModel.MapViewModel.currentPlace.Longitude);

            PlacesMapLayer.Clear();
            PlacesMapLayer.Add(diamondOverlay);
        }

        private void InitializeLocation()
        {
            geolocator = new Geolocator();
            geolocator.DesiredAccuracy = PositionAccuracy.High;
            geolocator.MovementThreshold = 10; // The units are meters.

            geolocator.StatusChanged += geolocator_StatusChanged;
            geolocator.PositionChanged += geolocator_PositionChanged;
        }

        void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            Dispatcher.BeginInvoke(() =>
            {
                //LatitudeTextBlock.Text = args.Position.Coordinate.Latitude.ToString("0.00000000");
                //LongitudeTextBlock.Text = args.Position.Coordinate.Longitude.ToString("0.00000000");

                GeoCoordinate currentCoordinate = new GeoCoordinate(args.Position.Coordinate.Latitude,
                    args.Position.Coordinate.Longitude);
                currCoordinate = currentCoordinate;

                MainMap.SetView(currentCoordinate, MainMap.ZoomLevel);

                HeadingPath.Visibility = Visibility.Visible;
                SecretTestPath.Visibility = Visibility.Visible;

                GeoCoordinate diamondCoordinate = new GeoCoordinate(StaticViewModel.MapViewModel.currentPlace.Latitude,
                    StaticViewModel.MapViewModel.currentPlace.Longitude);

                if (StaticMethod.Distance(currentCoordinate, diamondCoordinate) <= 0.1)
                {
                    DiamondControl tempDiamondControl = diamondOverlay.Content as DiamondControl;
                    tempDiamondControl.ChangeColor();

                    CollectGrid.Visibility = Visibility.Visible;

                }
                else
                {
                    CollectGrid.Visibility = Visibility.Collapsed;
                }

            });
        }

        void geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            string status = "";
            bool isShowLoading = false;
            int time = 0;

            switch (args.Status)
            {
                case PositionStatus.Disabled:
                    // the application does not have the right capability or the location master switch is off
                    status = "location is disabled in phone settings";
                    time = 2000;
                    break;
                case PositionStatus.Initializing:
                    // the geolocator started the tracking operation
                    status = "initializing...";
                    isShowLoading = true;
                    break;
                case PositionStatus.NoData:
                    // the location service was not able to acquire the location
                    status = "no data";
                    time = 2000;
                    break;
                case PositionStatus.Ready:
                    // the location service is generating geopositions as specified by the tracking parameters
                    status = "ready";
                    time = 2000;
                    break;
                case PositionStatus.NotAvailable:
                    status = "not available";
                    time = 2000;
                    // not used in WindowsPhone, Windows desktop uses this value to signal that there is no hardware capable to acquire location information
                    break;
                case PositionStatus.NotInitialized:
                    // the initial state of the geolocator, once the tracking operation is stopped by the user the geolocator moves back to this state
                    status = "Geo Locator not initialized";
                    time = 2000;
                    break;
            }

            Dispatcher.BeginInvoke(() =>
            {
                //StatusTextBlock.Text = status;
                StaticMethod.ShowProgress(this, status, time, isShowLoading, true);
            });
        }

        private void InitializeCompass()
        {
            if (!Compass.IsSupported)
            {
                // The device on which the application is running does not support
                // the compass sensor. Alert the user and hide the
                // application bar.
                //statusTextBlock.Text = "device does not support compass";

                MessageBox.Show("Device not supported compass sensors. Heading map will be turned off");
                HeadingPath.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Initialize the timer and add Tick event handler, but don't start it yet.
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(30);
                timer.Tick += new EventHandler(timer_Tick);

                //Start the compass
                if (compass != null && compass.IsDataValid)
                {
                    // Stop data acquisition from the compass.
                    compass.Stop();
                    timer.Stop();
                    //statusTextBlock.Text = "compass stopped.";
                }
                else
                {
                    if (compass == null)
                    {
                        // Instantiate the compass.
                        compass = new Compass();

                        // Specify the desired time between updates. The sensor accepts
                        // intervals in multiples of 20 ms.
                        compass.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);

                        // The sensor may not support the requested time between updates.
                        // The TimeBetweenUpdates property reflects the actual rate.
                        //timeBetweenUpdatesTextBlock.Text = compass.TimeBetweenUpdates.TotalMilliseconds + " ms";


                        compass.CurrentValueChanged += compass_CurrentValueChanged;
                        compass.Calibrate += compass_Calibrate;

                        try
                        {
                            //statusTextBlock.Text = "starting compass.";
                            compass.Start();
                            timer.Start();
                        }
                        catch (InvalidOperationException)
                        {
                            //statusTextBlock.Text = "unable to start compass.";
                        }
                    }
                }
            }
        }

        private void compass_CurrentValueChanged(object sender, SensorReadingEventArgs<CompassReading> e)
        {
            // Note that this event handler is called from a background thread
            // and therefore does not have access to the UI thread. To update 
            // the UI from this handler, use Dispatcher.BeginInvoke() as shown.
            // Dispatcher.BeginInvoke(() => { statusTextBlock.Text = "in CurrentValueChanged"; });


            isDataValid = compass.IsDataValid;

            trueHeading = e.SensorReading.TrueHeading;
            magneticHeading = e.SensorReading.MagneticHeading;
            Dispatcher.BeginInvoke(() =>
            {
                //MagneticHeading.Text = magneticHeading.ToString();
                MainMap.Heading = Math.Round(magneticHeading, 0);
            });
            headingAccuracy = Math.Abs(e.SensorReading.HeadingAccuracy);
            rawMagnetometerReading = e.SensorReading.MagnetometerReading;

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!calibrating)
            {
                if (isDataValid)
                {
                    //statusTextBlock.Text = "receiving data from compass.";
                }

                // Update the textblocks with numeric heading values
                //magneticTextBlock.Text = magneticHeading.ToString("0.0");
                //trueTextBlock.Text = trueHeading.ToString("0.0");
                //accuracyTextBlock.Text = headingAccuracy.ToString("0.0");

                // Update the line objects to graphically display the headings
                //double centerX = headingGrid.ActualWidth/2.0;
                //double centerY = headingGrid.ActualHeight/2.0;
                //magneticLine.X2 = centerX - centerY*Math.Sin(MathHelper.ToRadians((float) magneticHeading));
                //magneticLine.Y2 = centerY - centerY*Math.Cos(MathHelper.ToRadians((float) magneticHeading));
                //trueLine.X2 = centerX - centerY*Math.Sin(MathHelper.ToRadians((float) trueHeading));
                //trueLine.Y2 = centerY - centerY*Math.Cos(MathHelper.ToRadians((float) trueHeading));

                // Update the textblocks with numeric raw magnetometer readings
                //xTextBlock.Text = rawMagnetometerReading.X.ToString("0.00");
                //yTextBlock.Text = rawMagnetometerReading.Y.ToString("0.00");
                //zTextBlock.Text = rawMagnetometerReading.Z.ToString("0.00");

                // Update the line objects to graphically display raw data
                //xLine.X2 = xLine.X1 + rawMagnetometerReading.X*4;
                //yLine.X2 = yLine.X1 + rawMagnetometerReading.Y*4;
                //zLine.X2 = zLine.X1 + rawMagnetometerReading.Z*4;
            }
            else
            {
                //TODO: Calibrate Compass
                CalibrationGrid.Visibility = Visibility.Collapsed;
                //if (headingAccuracy <= 15)
                //{
                //    calibrationTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                //    calibrationTextBlock.Text = "Complete!";
                //    calibrating = false;
                //    CalibrationGrid.Visibility = Visibility.Collapsed;
                //}
                //else
                //{
                //    calibrationTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                //    calibrationTextBlock.Text = headingAccuracy.ToString("0.0");
                //}
            }
        }

        private void compass_Calibrate(object sender, CalibrationEventArgs e)
        {
            Dispatcher.BeginInvoke(() => { CalibrationGrid.Visibility = Visibility.Visible; });
            calibrating = true;
        }

        private void ZoomToLocation(GeoCoordinate geo, double zoomLevel)
        {
            MainMap.Center = geo;
            MainMap.ZoomLevel = zoomLevel;
        }

        private void RelocateGrid_OnTap(object sender, GestureEventArgs e)
        {
            MainMap.SetView(currCoordinate, MainMap.ZoomLevel);
            HeadingPath.Visibility = Visibility.Visible;
            SecretTestPath.Visibility = Visibility.Visible;
        }

        private void CollectGrid_OnTap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageGroups/MapGroup/CollectPage.xaml", UriKind.Relative));
        }

        private void SecretTestPath_OnTap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageGroups/MapGroup/CollectPage.xaml", UriKind.Relative));
            CollectGrid.Visibility = Visibility.Visible;
        }

        private void MainMap_OnCenterChanged(object sender, MapCenterChangedEventArgs e)
        {
            if (geolocator.LocationStatus == PositionStatus.Ready)
            {
                if (StaticMethod.Distance(currCoordinate, MainMap.Center) > 0.2)
                {
                    HeadingPath.Visibility = Visibility.Collapsed;
                    SecretTestPath.Visibility = Visibility.Collapsed;
                }
                else
                {
                    HeadingPath.Visibility = Visibility.Visible;
                    SecretTestPath.Visibility = Visibility.Visible;
                }
            }
        }
    }
}