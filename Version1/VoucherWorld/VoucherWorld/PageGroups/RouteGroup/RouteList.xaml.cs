using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Navigation;
using VoucherWorld.Model;
using VoucherWorld.ViewModel;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace VoucherWorld.PageGroups.RouteGroup
{
    public partial class RouteList : PhoneApplicationPage
    {

        string cat;

        public RouteList()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //TODO: do request base on category
            await StaticViewModel.RouteListViewModel.LoadData(cat);
            RouteListBox.ItemsSource = StaticViewModel.RouteListViewModel.RouteCollection;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            if (NavigationContext.QueryString.TryGetValue("cat", out cat))
            {
                string name = cat;
            }
        }

        private void RouteGrid_OnTap(object sender, GestureEventArgs e)
        {
            //TODO: navigate to RouteDetail page

            //Route selectedRoute = ((Grid) sender).Tag as Route;
            Route selectedRoute = RouteListBox.SelectedItem as Route;
            StaticData.currentRoute = selectedRoute;

            NavigationService.Navigate(
                new Uri("/PageGroups/RouteGroup/RouteDetails.xaml?RouteId=" + selectedRoute.Id, UriKind.Relative));
        }
    }
}