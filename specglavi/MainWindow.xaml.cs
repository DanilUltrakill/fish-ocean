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
        //List<FastFish> fastFish = new List<FastFish>();

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

        Options options = new Options();

        Random rnd = new Random();
        int cordx, cordy;

        private void fish_spawn(int fast, int stamina)
        {
            int[,] oxy;
            oxy = new int[18, 12];

            int[,] sxy;
            sxy = new int[18, 12];

            int[,] fxy;
            fxy = new int[18, 12];

            for (int ox = 0; ox < 18; ox++)
            {
                for (int oy = 0; oy < 12; oy++)
                {
                    oxy[ox, oy] = 0;
                }
            }                        
            
            //int[,] yy;
            //yy = new int[fast + stamina, fast + stamina];

            for (int i = stamina; i > 0; i--)
            {
                staminaf fish1 = new staminaf();

                fish1.fx = (rnd.Next(1, 18) * 50);
                fish1.fy = (rnd.Next(1, 12) * 50);

                fish1.fishsr.Margin = new Thickness(fish1.fx, fish1.fy, 0, 0);
                scene.Children.Add(fish1.fishsr);
                staminafish.Add(fish1);

                for (int xi = 0; xi < 18; xi++)
                {
                    for (int yi = 0; yi < 12; yi++)
                    {
                        if (xi == (fish1.fx / 50))
                        {
                            if (yi == (fish1.fy / 50))
                            {
                                oxy[xi, yi] = 1;
                            }
                        }
                    }
                }

                //xx = fish1.fx;
                //yy = fish1.fy;
                //if (xx == fish1.fx)
                //    fish1.fx = (rnd.Next(1, 18) * 50);
                //if (yy == fish1.fy)
                //    fish1.fy = (rnd.Next(1, 18) * 50);
            }

            for (int i = fast; i > 0; i--)
            {
                fastf fish2 = new fastf();

                fish2.fx = (rnd.Next(1, 18) * 50);
                fish2.fy = (rnd.Next(1, 12) * 50);

                fish2.fishfr.Margin = new Thickness(fish2.fx, fish2.fy, 0, 0);
                scene.Children.Add(fish2.fishfr);
                fastfish.Add(fish2);

                for (int xi = 0; xi < 18; xi++)
                {
                    for (int yi = 0; yi < 12; yi++)
                    {
                        if (xi == (fish2.fx / 50))
                        {
                            if (yi == (fish2.fy / 50))
                            {
                                fxy[xi, yi] = 1;
                            }
                        }
                        //if (xi != fxi)
                        //{
                        //    if (yi != fyi)
                        //    {
                        //        fish1.fishr.Margin = new Thickness(fish1.fx, fish1.fy, 0, 0);
                        //        scene.Children.Add(fish1.fishr);
                        //    }
                        //}

                    }
                }

                for (int xi = 0; xi < 18; xi++)
                {
                    for (int yi = 0; yi < 12; yi++)
                    {
                        if (oxy[xi, yi] != fxy[xi, yi])
                        {
                            
                        }
                    }
                }
            }
        }

        private void tango_spawn(int amountoftango)
        {
            for (int i = amountoftango; i > 0; i--)
            {
                Rectangle tango = new Rectangle();

                cordx = (rnd.Next(1, 18) * 50);
                cordy = (rnd.Next(1, 12) * 50);

                tango.Height = 25;
                tango.Width = 50;
                ImageBrush ib = new ImageBrush();
                ib.AlignmentX = AlignmentX.Left;
                ib.Stretch = Stretch.None;
                ib.Viewbox = new Rect(0, 0, 50, 25);
                ib.ViewboxUnits = BrushMappingMode.Absolute;
                ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/pictures/12_TANGOS.png", UriKind.Absolute));
                tango.Fill = ib;
                tango.Margin = new Thickness(cordx, cordy, 0, 0);
                scene.Children.Add(tango);
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

