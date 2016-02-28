using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using VoucherWorld.Model;
using VoucherWorld.ViewModel;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace VoucherWorld.PageGroups.HistoryGroup
{
    public partial class HistoryPage : PhoneApplicationPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        private void HistoryGrid_OnTap(object sender, GestureEventArgs e)
        {
            Enrrollments selectedErrollments = HistoryListBox.SelectedItem as Enrrollments;

            StaticViewModel.MapViewModel.RouteDetailsModel = selectedErrollments;

            StaticData.CurrentErrollments = selectedErrollments;

            StaticData.isHistory = true;

            NavigationService.Navigate(
                new Uri("/PageGroups/RouteGroup/RouteDetails.xaml?RouteId=" + selectedErrollments.Id,
                    UriKind.Relative));
        }

        private void HistoryPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            StaticData.isHistory = false;
            HistoryListBox.ItemsSource = StaticData.ErrollmentHistory;
        }
    }
}