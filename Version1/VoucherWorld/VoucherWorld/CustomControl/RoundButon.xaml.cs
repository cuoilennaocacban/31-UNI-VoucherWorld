using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VoucherWorld.CustomControl
{
    public partial class RoundButon : UserControl
    {
        public RoundButon()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public static readonly DependencyProperty BindingSizeProperty =
            DependencyProperty.Register("BothSize", typeof(double), typeof(RoundButon),
                new PropertyMetadata(default(double)));

        public double BothSize
        {
            get { return (double)GetValue(BindingSizeProperty); }
            set { SetValue(BindingSizeProperty, value); }
        }

        public static readonly DependencyProperty BindingContentSizeProperty =
            DependencyProperty.Register("ContentSize", typeof(double), typeof(RoundButon),
                new PropertyMetadata(default(double)));

        public double ContentSize
        {
            get { return (double)GetValue(BindingContentSizeProperty); }
            set { SetValue(BindingContentSizeProperty, value); }
        }

        public static readonly DependencyProperty BindingContentDataProperty =
            DependencyProperty.Register("ContentData", typeof(string), typeof(RoundButon),
                new PropertyMetadata(default(string)));

        public string ContentData
        {
            get { return (string)GetValue(BindingContentDataProperty); }
            set { SetValue(BindingContentDataProperty, value); }
        }

        public static readonly DependencyProperty BindingColorProperty =
            DependencyProperty.Register("BorderBackground", typeof(Brush), typeof(RoundButon),
                new PropertyMetadata(default(Brush)));

        public Brush BorderBackground
        {
            get { return (Brush)GetValue(BindingColorProperty); }
            set { SetValue(BindingColorProperty, value); }
        }

        public static readonly DependencyProperty InnerRotationProperty =
            DependencyProperty.Register("InnerRotation", typeof (double), typeof (RoundButon),
                new PropertyMetadata(default(double)));

        public double InnerRotation
        {
            get { return (double) GetValue(InnerRotationProperty); }
            set { SetValue(InnerRotationProperty, value); }
        }
    }
}
