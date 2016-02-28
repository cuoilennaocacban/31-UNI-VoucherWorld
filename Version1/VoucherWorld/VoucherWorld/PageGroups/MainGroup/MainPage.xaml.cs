using Microsoft.Phone.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using VoucherWorld.CustomControl;
using VoucherWorld.Model;
using VoucherWorld.Utilities;
using VoucherWorld.ViewModel;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace VoucherWorld
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool isOpened = false;

        private ObservableCollection<Category> _catCollection = new ObservableCollection<Category>();

        private bool isAnimatingOut = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            this.Loaded += OnLoaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavigationService.RemoveBackEntry();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_catCollection.Count == 0)
            {
                AddCategory();
            }

            StaticData.isHistory = false;
            //HistoryListBox.ItemsSource = StaticData.ErrollmentHistory;
        }

        private void AddCategory()
        {
            Category newCat = new Category();
            newCat.Name = "Food";
            newCat.Path = "M3.0066905,8.6380036C16.363398,13.427042 13.233119,18.908087 13.233119,18.908087 13.05152,19.22609 13.11462,19.699093 13.373818,19.958096L32.792884,39.376251C34.852868,44.243294 31.001296,42.074276 31.001296,42.074276 30.682199,41.894272 30.208301,41.535271 29.949103,41.276268L10.622438,21.947111C3.3347883,23.699126 0.032711029,12.436034 0.032711029,12.436034 -0.070088387,12.084031 0.07761097,11.986031 0.36080933,12.219033L7.082062,17.723078C7.3652602,17.955078,7.4212598,17.902079,7.2058613,17.606076L2.0378976,10.500018C1.8223987,10.203017,1.8815985,10.146015,2.1699967,10.372017L8.8744499,15.65106C9.1628477,15.878063,9.2026474,15.836062,8.9636491,15.55806z M11.483635,0L41.825421,30.343672C41.825421,30.343672,44.460866,34.262764,39.404973,32.764714L22.535513,15.893348 18.583792,15.891364 8.8899875,5.7951386C8.8899873,5.7951386,6.3536388,0.59303035,11.483635,0z";

            _catCollection.Add(newCat);

            Category newCat2 = new Category();
            newCat2.Name = "Book";
            newCat2.Path = "M18.698001,56.938L18.698001,59.728001 31.349,59.728001 31.349,56.938z M1.8610001,56.938L1.8610001,59.727997 11.535001,59.727997 11.535001,56.938z M59.898386,55.058998L45.885002,57.419712 46.34852,60.171997 60.362001,57.811481z M52.786489,12.724999L38.773001,15.086076 39.236515,17.838997 53.250002,15.477921z M1.8610001,10.271L1.8610001,13.061001 11.535001,13.061001 11.535001,10.271z M54.079065,9.757L62.307001,61.243523 45.055634,64 36.829,12.513545z M0,5.21L13.394,5.21 13.394,64 0,64z M18.698001,4.9379997L18.698001,7.7279997 31.349,7.7279997 31.349,4.9379997z M17.023001,0L33.023001,0 33.023001,64 17.023001,64z";

            _catCollection.Add(newCat2);

            CategoryListBox.ItemsSource = _catCollection;
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

        private void CategoryGrid_OnTap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageGroups/RouteGroup/RouteList.xaml", UriKind.Relative));
        }

        private void HistoryGrid_OnTap(object sender, GestureEventArgs e)
        {
            //Enrrollments selectedErrollments = HistoryListBox.SelectedItem as Enrrollments;

            ////StaticViewModel.MapViewModel.RouteDetailsModel = selectedErrollments;

            //StaticData.CurrentErrollments = selectedErrollments;

            //StaticData.isHistory = true;

            //NavigationService.Navigate(
            //    new Uri("/PageGroups/RouteGroup/RouteDetails.xaml?RouteId=" + selectedErrollments.Id,
            //        UriKind.Relative));
        }

        private void SettingMenuBarItem_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageGroups/SettingsGroup/SettingsPage.xaml", UriKind.Relative));
        }

        private void AboutMenuBarItem_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageGroups/Common/AboutGroup/about.xaml", UriKind.Relative));
        }

        private void RateAndReviewMenuBarItem_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageGroups/Common/RateAndReviewGroup/RateAndReviewPage.xaml", UriKind.Relative));
        }

        private void BigRoundButton_Tap(object sender, GestureEventArgs e)
        {
            if (!isOpened)
            {
                StoryboardOpen.Begin();
                isOpened = true;
            }
            else
            {
                StoryboardClose.Begin();
                isOpened = false;
            }
        }

        private void RoundButon_OnTap(object sender, GestureEventArgs e)
        {
            RoundButon selectedButon = sender as RoundButon;
            string temp = selectedButon.Tag.ToString();

            NavigationService.Navigate(new Uri("/PageGroups/RouteGroup/RouteList.xaml?cat=" + temp, UriKind.Relative));
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
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

        private void UserInfogMenuBarItem_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageGroups/UserInfoGroup/UserInfoPage.xaml", UriKind.Relative));
        }

        private void HistoryBarMenuItem_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageGroups/HistoryGroup/HistoryPage.xaml", UriKind.Relative));
        }

        private void MagicalLens_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageGroups/MagicLenPage.xaml", UriKind.Relative));
        }
    }
}