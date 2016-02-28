using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VoucherWorld.CustomControl
{
    public partial class BigRoundButton : UserControl
    {
        public BigRoundButton()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public static readonly DependencyProperty BindingSizeProperty =
            DependencyProperty.Register("BothSize", typeof (double), typeof (BigRoundButton),
                new PropertyMetadata(default(double)));

        public double BothSize
        {
            get { return (double)GetValue(BindingSizeProperty); }
            set { SetValue(BindingSizeProperty, value); }
        }

        public static readonly DependencyProperty BindingContentSizeProperty =
            DependencyProperty.Register("ContentSize", typeof (double), typeof (BigRoundButton),
                new PropertyMetadata(default(double)));

        public double ContentSize
        {
            get { return (double)GetValue(BindingContentSizeProperty); }
            set { SetValue(BindingContentSizeProperty, value); }
        }

        public static readonly DependencyProperty BindingContentDataProperty =
            DependencyProperty.Register("ContentData", typeof(string), typeof(BigRoundButton),
                new PropertyMetadata(default(string)));

        public string ContentData
        {
            get { return (string)GetValue(BindingContentDataProperty); }
            set { SetValue(BindingContentDataProperty, value); }
        }

        private void LayoutRoot_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            VisualStateManager.GoToState(this, Tapped.Name, false);
            Debug.WriteLine("tapped");
        }


        private void LayoutRoot_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            VisualStateManager.GoToState(this, UnTapped.Name, false);
            Debug.WriteLine("UnTapped");
        }
    }
}
