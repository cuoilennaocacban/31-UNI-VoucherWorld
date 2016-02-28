using System.Windows.Media;
using Microsoft.Phone.Controls;

namespace VoucherWorld.PageGroups.Common.AboutGroup
{
    public partial class about : PhoneApplicationPage
    {
        private TranslateTransform translateTransform;

        public about()
        {
            InitializeComponent();
            translateTransform = new TranslateTransform();
            spinningSquare.RenderTransform = translateTransform;
        }

        private void GestureListener_DragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            translateTransform.X += e.HorizontalChange;
            translateTransform.Y += e.VerticalChange;
        }
    }
}