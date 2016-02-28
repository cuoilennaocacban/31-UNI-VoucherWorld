using System.Windows.Controls;

namespace VoucherWorld.CustomControl
{
    public partial class SpinningSquare : UserControl
    {
        public SpinningSquare()
        {
            InitializeComponent();
            SpinningSquareStoryBoard.Begin();
        }
    }
}
