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
using System.Windows.Shapes;

namespace Fish_ocean
{
    /// <summary>
    /// Логика взаимодействия для Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();
        }

        public string fast, stamina, tango;

        public void Ok_Click(object sender, RoutedEventArgs e)
        {
            fast = fastfish.Text;
            stamina = staminafish.Text;
            tango = tangoamount.Text;            
        }
    }
}
