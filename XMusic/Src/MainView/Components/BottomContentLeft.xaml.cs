using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XMusic.Src.MainView.Components
{
    /// <summary>
    /// BottomContentLeft.xaml 的交互逻辑
    /// </summary>
    public partial class BottomContentLeft : UserControl
    {
        public BottomContentLeft()
        {
            InitializeComponent();
            var storyboard = (Storyboard)FindResource("RotateAnimation");
            storyboard.Begin();
        }
    }
}
