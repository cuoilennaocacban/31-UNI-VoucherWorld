using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GART;
using GART.BaseControls;
using GART.Controls;
using GART.Data;
using GART.X3D;
using HtmlAgilityPack;
using Microsoft.Devices.Sensors;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Globalization;
using System.Windows;
using TestGART.CustomControl;
using TestGART.Model;
using TestGART.Utilities;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;
using Vector3 = Microsoft.Xna.Framework.Vector3;

namespace TestGART
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ObservableCollection<ARItem> locationsTvrda;
        private ObservableCollection<Cat> catCollection;

        private Accelerometer _sensor;

        private MapLayer DiamondLayer;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            //if (!Accelerometer.IsSupported)
            //{
            //    // The device on which the application is running does not support
            //    // the accelerometer sensor. Alert the user and hide the
            //    // application bar.
            //    MessageBox.Show("device does not support compass");
            //    ApplicationBar.IsVisible = false;
            //}
            //else
            //{
            //    SetAccelerometer();
            //}
            locationsTvrda = new ObservableCollection<ARItem>();
            overheadMap.Loaded += OverheadMapOnLoaded;
        }

        private void SetAccelerometer()
        {
            // Instantiate the accelerometer.
            _sensor = new Accelerometer();
            // Specify the desired time between updates. The sensor accepts
            // intervals in multiples of 20 ms.
            _sensor.TimeBetweenUpdates = TimeSpan.FromMilliseconds(40);
            // The sensor may not support the requested time between updates.
            // The TimeBetweenUpdates property reflects the actual rate.
            _sensor.CurrentValueChanged += new EventHandler<SensorReadingEventArgs
             <AccelerometerReading>>(SensorCurrentValueChanged);
            _sensor.Start();
        }

        private const double EdgeGlassAngle = 20.0 * Math.PI / 180.0;
        private const double UsableLateralAmplitude = 400;

        private void SensorCurrentValueChanged(object sender, SensorReadingEventArgs
            <AccelerometerReading> e)
        {
            // Note that this event handler is called from a background thread
            this.Dispatcher.BeginInvoke(() =>
            {
                Vector3 vec = e.SensorReading.Acceleration;
                if (vec.Z < -0.8 && StaticData.mode == 1)
                {
                    //Enable map
                    UIHelper.ToggleVisibility(overheadMap);
                    StaticData.mode = 0;
                }
                else
                {
                    if (vec.Z > -0.8 && StaticData.mode == 0)
                    {
                        //Enable Camera
                        UIHelper.ToggleVisibility(overheadMap);
                        StaticData.mode = 1;
                    }
                }
            });

        }

        private void OverheadMapOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (overheadMap.Map != null)
            {

                GetData();

                overheadMap.Map.Layers.Clear();
                DiamondLayer = new MapLayer();

                DisplayData(DiamondLayer);

                overheadMap.Map.Layers.Add(DiamondLayer);
            }
        }

        private void GetData()
        {

            string data = "";
            //XDocument doc;

            //using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            //{

            //    //if (isf.FileExists("VoucherWolrdSample.xml"))
            //    //{
            //    //    using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream("VoucherWolrdSample.xml", FileMode.Open, isf))
            //    //    {
            //    //        doc = XDocument.Load(isfs);
            //    //    }
            //    //}
            //    //else
            //    //{
            //        using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream("VoucherWolrdSample.xml", FileMode.Create, isf))
            //        {
            //            //doc = XDocument.Load("Data/SavedImage.xml");
            //            HtmlDocument hdoc = new HtmlDocument();
            //            hdoc.Load("Data/VoucherWolrdSample.xml");
            //            //doc.Save(isfs);
            //        }

            //    //    using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream("VoucherWolrdSample.xml", FileMode.Open, isf))
            //    //    {
            //    //        doc = XDocument.Load(isfs);
            //    //    }
            //    //}
            //}



            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.Load("Data/VoucherWolrdSample.xml");

            catCollection = new ObservableCollection<Cat>();
            foreach (HtmlNode catItem in htmlDoc.DocumentNode.SelectNodes("//cat"))
            {
                Cat newCat = new Cat();
                newCat.Name = catItem.Attributes["name"].Value;
                newCat.Places = new ObservableCollection<Place>();
                foreach (HtmlNode placeNode in catItem.SelectNodes(".//place"))
                {
                    Place newPlace = new Place();
                    newPlace.Name = placeNode.Attributes["name"].Value;
                    newPlace.Add = placeNode.Attributes["add"].Value;
                    newPlace.Id = placeNode.Attributes["id"].Value;
                    newPlace.Diamonds = new ObservableCollection<Diamond>();
                    foreach (HtmlNode diamondNode in placeNode.SelectNodes(".//diamond"))
                    {
                        Diamond newDiamond = new Diamond();
                        newDiamond.No = Convert.ToInt32(diamondNode.Attributes["no"].Value);
                        double lat = Convert.ToDouble(diamondNode.ChildNodes["point"].Attributes["lat"].Value,
                            NumberFormatInfo.InvariantInfo);
                        double lon = Convert.ToDouble(diamondNode.ChildNodes["point"].Attributes["lon"].Value,
                            NumberFormatInfo.InvariantInfo);
                        double high = Convert.ToDouble(diamondNode.ChildNodes["point"].Attributes["high"].Value,
                            NumberFormatInfo.InvariantInfo);
                        newDiamond.Point = new GeoCoordinate(lat, lon, high);

                        newPlace.Diamonds.Add(newDiamond);
                    }

                    newCat.Places.Add(newPlace);
                }

                catCollection.Add(newCat);
            }

            foreach (Cat catItem in catCollection)
            {
                foreach (Place placeItem in catItem.Places)
                {
                    foreach (Diamond diamondItem in placeItem.Diamonds)
                    {
                        CityPlace newCityPlace = new CityPlace();
                        newCityPlace.GeoLocation = diamondItem.Point;
                        newCityPlace.Description = placeItem.Name;
                        newCityPlace.Content = placeItem.Add;
                        newCityPlace.Diamond = diamondItem;

                        locationsTvrda.Add(newCityPlace);
                    }
                }
            }

            ardisplay.ARItems = locationsTvrda;
        }

        private void DisplayData(MapLayer layer)
        {
            foreach (Cat cat in catCollection)
            {
                foreach (Place place in cat.Places)
                {

                    MapPolyline newPolyline = new MapPolyline();
                    newPolyline.StrokeColor = Colors.Magenta;
                    newPolyline.StrokeThickness = 5;
                    newPolyline.Path = new GeoCoordinateCollection();

                    foreach (Diamond diamond in place.Diamonds)
                    {
                        MapOverlay newMapOverlay = new MapOverlay();
                        newMapOverlay.Content = new DiamondControl(diamond);
                        newMapOverlay.GeoCoordinate = diamond.Point;
                        newMapOverlay.PositionOrigin = new Point(0, 0.5);
                        layer.Add(newMapOverlay);

                        newPolyline.Path.Add(diamond.Point);
                    }

                    overheadMap.Map.MapElements.Add(newPolyline);
                }
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            ardisplay.StartServices();
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            ardisplay.StopServices();
            base.OnNavigatedFrom(e);
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
        private void ThreeDButton_OnClick(object sender, EventArgs e)
        {
            UIHelper.ToggleVisibility(worldView);
        }

        private void HeadingButton_OnClick(object sender, EventArgs e)
        {
            UIHelper.ToggleVisibility(headingIndicator);
        }

        private void MapButton_OnClick(object sender, EventArgs e)
        {
            UIHelper.ToggleVisibility(overheadMap);
            StaticData.mode = StaticData.mode == 0 ? 1 : 0;
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

        private void CityItem_OnTap(object sender, GestureEventArgs e)
        {
            if (MessageBox.Show("Get Diamond?", "Congratulation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                CityPlace selectedCityPlace = ((Grid) sender).Tag as CityPlace;
                locationsTvrda.Remove(selectedCityPlace);

                foreach (MapOverlay mapOverlay in DiamondLayer)
                {
                    if (((DiamondControl)mapOverlay.Content).diamond == selectedCityPlace.Diamond)
                    {
                        DiamondLayer.Remove(mapOverlay);
                        break;
                    }
                }

                if (selectedCityPlace.Diamond.No == 3)
                {
                    NavigationService.Navigate(new Uri("/GiftCodePage.xaml", UriKind.Relative));
                }
            }
        }
    }
}