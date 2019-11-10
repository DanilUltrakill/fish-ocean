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
    class fastf : tango
    {
        public fastf()
        {
            fishfr.Height = 50;
            fishfr.Width = 50;

            ibfishfr.AlignmentX = AlignmentX.Left;
            ibfishfr.AlignmentY = AlignmentY.Top;
            ibfishfr.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/pictures/FAST_AND_FURIOUS.png", UriKind.Absolute));
            fishfr.Fill = ibfishfr;
        }
    }
}
