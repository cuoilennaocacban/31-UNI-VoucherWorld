using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace TestGART
{
    public partial class GiftCodePage : PhoneApplicationPage
    {
        public GiftCodePage()
        {
            InitializeComponent();
        }

        private void UIElement_OnTap(object sender, GestureEventArgs e)
        {
            icecream.Visibility = Visibility.Collapsed;
            gift.Visibility = Visibility.Visible;
            GiftCodeTextBlock.Text = "125FHR3";
        }
    }
}