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

namespace Fish_ocean
{
    class staminaf : fish1
    {
        Random rnd = new Random();
        //private int speed;
        //private int age;
        public staminaf()
        {
            //age = rnd.Next(1, 10);
            fishsr.Height = 50;
            fishsr.Width = 50;

            ibfishsr.AlignmentX = AlignmentX.Left;
            ibfishsr.AlignmentY = AlignmentY.Top;
            ibfishsr.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/pictures/roflanRibalo.gif", UriKind.Absolute));
            fishsr.Fill = ibfishsr;
        }
    }
}
