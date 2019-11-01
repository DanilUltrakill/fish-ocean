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
    class tango
    {
        public int tx, ty;
        public Rectangle tangofr = new Rectangle();
        public ImageBrush ibtangofr = new ImageBrush();     
        
        public tango()
        {
            tangofr.Height = 50;
            tangofr.Width = 50;

            ibtangofr.AlignmentX = AlignmentX.Left;
            ibtangofr.AlignmentY = AlignmentY.Top;
            ibtangofr.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/pictures/12_TANGOS.gif", UriKind.Absolute));
            tangofr.Fill = ibtangofr;
        }
    }    
}
