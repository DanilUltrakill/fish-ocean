using specglavi;
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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<fastf> fastfish = new List<fastf>();
        List<staminaf> staminafish = new List<staminaf>();
        List<tango> tangos = new List<tango>();

        public MainWindow()
        {
            InitializeComponent();

            ImageBrush ib = new ImageBrush();

            //ib.AlignmentX = AlignmentX.Left;
            //ib.Stretch = Stretch.None;
            //ib.ViewboxUnits = BrushMappingMode.Absolute;
            ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/pictures/ocean.jpg", UriKind.Absolute));
            scene.Background = ib;
        }

        int[,] fxy = new int[19, 13];



        Options options = new Options();

        Random rnd = new Random();
        int cordx, cordy;

        private void fish_spawn(int fast, int stamina)
        {

            for (int i = stamina; i > 0; i--)
            {
                staminaf fish1 = new staminaf();

                fish1.fx = (rnd.Next(1, 19) * 50);
                fish1.fy = (rnd.Next(1, 13) * 50);
                proverka(fish1.fx, fish1.fy, out fish1.fx, out fish1.fy);
                fish1.fishsr.Margin = new Thickness(fish1.fx, fish1.fy, 0, 0);
                scene.Children.Add(fish1.fishsr);
                staminafish.Add(fish1);

            }

            for (int i = fast; i > 0; i--)
            {
                fastf fish2 = new fastf();

                fish2.fx = (rnd.Next(1, 19) * 50);
                fish2.fy = (rnd.Next(1, 13) * 50);
                proverka(fish2.fx, fish2.fy, out fish2.fx, out fish2.fy);
                fish2.fishfr.Margin = new Thickness(fish2.fx, fish2.fy, 0, 0);
                scene.Children.Add(fish2.fishfr);
                fastfish.Add(fish2);


            }
        }
        public void proverka(int a, int b, out int x, out int y)
        {


            x = a / 50;
            y = b / 50;
            while (fxy[x, y] == 1)
            {
                x = rnd.Next(1, 19);
                y = rnd.Next(1, 13);
            }

            if (fxy[x, y] == 0)
            {
                fxy[x, y] = 1;
                x = x * 50;
                y = y * 50;
            }
        }
        private void tango_spawn(int amountoftango)
        {
            for (int i = amountoftango; i > 0; i--)
            {
                tango tango = new tango();

                tango.tx = (rnd.Next(1, 19) * 50);
                tango.ty = (rnd.Next(1, 13) * 50);
                proverka(tango.tx, tango.ty, out tango.tx, out tango.ty);
                tango.tangofr.Margin = new Thickness(tango.tx, tango.ty, 0, 0);
                scene.Children.Add(tango.tangofr);
                tangos.Add(tango);
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            int fast = int.Parse(options.fast);
            int stamina = int.Parse(options.stamina);
            int tango = int.Parse(options.tango);
            fish_spawn(fast, stamina);
            tango_spawn(tango);
            fast = 0;
            stamina = 0;
            tango = 0;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            for (int j = 0; j < 19; j++)
            {
                for (int i = 0; i < 13; i++)
                {
                    fxy[j, i] = 0;
                }
            }
            scene.Children.Clear();
            options.Show();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            options.Close();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            options.Show();
        }

    }
}

