using System.Windows.Controls;
using TestGART.Model;

namespace TestGART.CustomControl
{
    public partial class DiamondControl : UserControl
    {
        public Diamond diamond;

        public DiamondControl()
        {
            InitializeComponent();
        }

        public DiamondControl(Diamond iniDiamond)
        {
            diamond = iniDiamond;
            InitializeComponent();
        }
    }
}
