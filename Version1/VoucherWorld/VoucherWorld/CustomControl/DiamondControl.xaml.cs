using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace VoucherWorld.CustomControl
{
    public partial class DiamondControl : UserControl
    {
        public DiamondControl()
        {
            InitializeComponent();
            this.Loaded += DiamondControl_Loaded;
        }

        void DiamondControl_Loaded(object sender, RoutedEventArgs e)
        {
            StoryboardGlow.Begin();
        }

        public void ChangeColor()
        {
            StoryboardGlow_Blue.Begin();
        }
    }
}
