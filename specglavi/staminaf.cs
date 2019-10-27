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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace specglavi
{
    class staminaf : fish
    {
        public staminaf()
        {
            fishsr.Height = 50;
            fishsr.Width = 50;

            ibfishsr.AlignmentX = AlignmentX.Left;
            ibfishsr.AlignmentY = AlignmentY.Top;
            ibfishsr.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/pictures/roflanRibalo.png", UriKind.Absolute));
            fishsr.Fill = ibfishsr;
        }
    }
}
