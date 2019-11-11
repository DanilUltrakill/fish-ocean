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
        List<fish> fish = new List<fish>();

        System.Windows.Threading.DispatcherTimer FastRibki;
        System.Windows.Threading.DispatcherTimer StaminaRibki;

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
        int round;

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
                fish.Add(fish1);
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
                fish.Add(fish2);
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

            if (round == 0)
            {
                fish_spawn(fast, stamina);
                tango_spawn(tango);
            }
            else
            {
                //int f=0, s=0;
                foreach (fish fish in fish)
                {
                    int del = -1;
                    int cur = -1;
                    foreach (fastf ff in fastfish)
                    {
                        cur++;
                        if (ff.pregnant < 2)
                        {
                            del = cur;
                            scene.Children.Remove(ff.fishfr);
                        }
                    }
                    if (del!=-1)
                    {
                        fastfish.RemoveAt(del);
                    }
                }
                foreach (fish fish in fish)
                {
                    int del = -1;
                    int cur = -1;
                    foreach (staminaf sf in staminafish)
                    {
                        cur++;
                        if (sf.pregnant == 0)
                        {
                            del = cur;
                            scene.Children.Remove(sf.fishsr);
                        }
                    }
                    if (del != -1)
                    {
                        staminafish.RemoveAt(del);
                    }                    
                }
                foreach (staminaf sf in staminafish)
                {
                    sf.pregnant = 0;
                }
                foreach (fastf ff in fastfish)
                {
                    ff.pregnant = 0;
                }
                //foreach (fastf ff in fastfish)
                //{
                //    if (ff.pregnant == true)
                //        f++;
                //}
                //foreach (staminaf sf in staminafish)
                //{
                //    if (sf.pregnant == true)
                //        s++;
                //}
                //fish_spawn(f, s);
                tango_spawn(tango);
            }
            round++;

            FastRibki = new System.Windows.Threading.DispatcherTimer();
            FastRibki.Tick += new EventHandler(FastRibki_Tick);
            FastRibki.Interval = new TimeSpan(0,0,0,1);

            StaminaRibki = new System.Windows.Threading.DispatcherTimer();
            StaminaRibki.Tick += new EventHandler(StaminaRibki_Tick);
            StaminaRibki.Interval = new TimeSpan(0, 0, 0, 2);

            FastRibki.Start();
            StaminaRibki.Start();
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

        int tx, ty;
        double cur;
        int tangofast, tangostam;

        private void FastRibki_Tick(object sender, EventArgs e)
        {
            if(!tangos.Any())
            {
                FastRibki.Stop();
                StaminaRibki.Stop();
            }

            int curfastfish = -1;

            foreach (fastf ff in fastfish)
            {
                double MinDistance1 = 1081.665;
                int curtango = -1;
                curfastfish++;
                foreach (tango tango in tangos)
                {
                    curtango++;
                    cur = Math.Sqrt(Math.Pow(ff.fx - tango.tx, 2) + Math.Pow(ff.fy - tango.ty, 2));
                    if (cur<MinDistance1)
                    {
                        MinDistance1 = cur;
                        tx = tango.tx;
                        ty = tango.ty;
                        tangofast = curtango;
                    }

                    if (ff.fx == tango.tx & ff.fy == tango.ty)
                    {
                        scene.Children.Remove(tango.tangofr);
                        ff.pregnant ++;
                    }
                }

                try
                {
                    if (ff.fx == tx & ff.fy == ty)
                    {
                        tangos.RemoveAt(tangofast);
                    }
                }
                catch(ArgumentOutOfRangeException)
                {

                }

                //тут как рыбка будет двигаться к танго
                if (ff.fy == ty)//rl
                {
                    if (ff.fx < tx)//r
                    {
                        if (ff.fx != tx)
                        {
                            ff.fx += 50;
                        }
                    }

                    if (ff.fx > tx)//l
                    {
                        if (ff.fx != tx)
                        {
                            ff.fx -= 50;
                        }
                    }
                }

                if (ff.fx == tx)
                {
                    if (ff.fy < ty)//d
                    {
                        if (ff.fy != ty)
                        {
                            ff.fy += 50;
                        }
                    }

                    if (ff.fy > ty)//u
                    {
                        if (ff.fy != ty)
                        {
                            ff.fy -= 50;
                        }
                    }
                }

                if (ff.fx < tx) //r
                {
                    if (ff.fy < ty)//d
                    {
                        if (ff.fx - ff.fy < tx - ty)//rightdown
                        {
                            if (ff.fx != (tx - ty + ff.fy))
                            {
                                ff.fx += 50;
                            }

                            if ((ff.fx == (tx - ty + ff.fy)) && ff.fx != tx)
                            {
                                ff.fx += 50;
                                ff.fy += 50;
                            }
                        }

                        if (ff.fx - ff.fy > tx - ty)//downright
                        {
                            if (ff.fy != (ff.fx - tx + ty))
                            {
                                ff.fy += 50;
                            }

                            if ((ff.fy != (ff.fx - tx + ty)) && ff.fy != ty)
                            {
                                ff.fx += 50;
                                ff.fy += 50;
                            }
                        }
                        if (ff.fx - ff.fy == tx - ty)//rddiag
                        {
                            if (ff.fy != ty)
                            {
                                ff.fx += 50;
                                ff.fy += 50;
                            }
                        }
                    }

                    //////////////////////////////////////////////

                    if (ff.fy > ty)//u
                    {
                        if (ff.fx + ff.fy < tx + ty)//rightup
                        {
                            if (ff.fx + ff.fy != (tx + ty - ff.fy))
                            {
                                ff.fx += 50;
                            }

                            if ((ff.fx + ff.fy == (tx + ty - ff.fy))&&ff.fx != tx)
                            {
                                ff.fx += 50;
                                ff.fy -= 50;
                            }
                        }

                        if (ff.fx + ff.fy > tx + ty)//upright
                        {
                            if (ff.fx + ff.fy != (tx + ty - ff.fx))
                            {
                                ff.fy -= 50;
                            }

                            if ((ff.fx + ff.fy == (tx + ty - ff.fx)) &&ff.fy != ty)
                            {
                                ff.fx += 50;
                                ff.fy -= 50;
                            }
                        }
                        if (Math.Sqrt(Math.Pow(ff.fx + ff.fy, 2)) == Math.Sqrt(Math.Pow(tx + ty, 2)))//rdiag
                        {
                            if (ff.fy != ty)
                            {
                                ff.fx += 50;
                                ff.fy -= 50;
                            }
                        }
                    }
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

                if (ff.fx > tx) //l
                {
                    if (ff.fy < ty)//d
                    {
                        if (ff.fx + ff.fy > tx + ty)//leftdown
                        {
                            if (ff.fx + ff.fy != (tx + ty - ff.fy))
                            {
                                ff.fx -= 50;
                            }

                            if ((ff.fx + ff.fy == (tx + ty - ff.fy))&&ff.fx != tx)
                            {
                                ff.fx -= 50;
                                ff.fy += 50;
                            }
                        }

                        if (ff.fx + ff.fy < tx + ty)//downleft
                        {
                            if (ff.fx + ff.fy != (tx + ty - ff.fx))
                            {
                                ff.fy += 50;
                            }

                            if ((ff.fx + ff.fy == (tx + ty - ff.fx))&&ff.fy != ty)
                            {
                                ff.fx -= 50;
                                ff.fy += 50;
                            }
                        }
                        if (ff.fx + ff.fy == tx + ty)//ldiag
                        {
                            if (ff.fy != ty)
                            {
                                ff.fx -= 50;
                                ff.fy += 50;
                            }
                        }
                    }

                    //////////////////////////////////////////////

                    if (ff.fy > ty)//u
                    {
                        if (ff.fx - ff.fy < tx - ty)//leftup
                        {
                            if (ff.fx != (tx - ty + ff.fy))
                            {
                                ff.fx -= 50;
                            }

                            if ((ff.fx == (tx - ty + ff.fy)) &&ff.fx != tx)
                            {
                                ff.fx -= 50;
                                ff.fy -= 50;
                            }
                        }

                        if (ff.fx - ff.fy > tx - ty)//upleft
                        {
                            if (ff.fx != (tx - ty + ff.fy))
                            {
                                ff.fy -= 50;
                            }

                            if ((ff.fx == (tx - ty + ff.fy)) &&ff.fy != ty)
                            {
                                ff.fx -= 50;
                                ff.fy -= 50;
                            }
                        }
                        if (ff.fx - ff.fy == tx - ty)//rddiag
                        {
                            if (ff.fy != ty)
                            {
                                ff.fx -= 50;
                                ff.fy -= 50;
                            }
                        }
                    }
                }

                scene.Children.Remove(ff.fishfr);
                ff.fishfr.Margin = new Thickness(ff.fx, ff.fy, 0, 0);
                scene.Children.Add(ff.fishfr);

                //if (ff.fx == tx & ff.fy == ty)
                //{
                //    scene.Children.Remove(ff.tangofr);
                //    tangos.RemoveAt(tangofast);
                //}
            }
        }

        private void StaminaRibki_Tick(object sender, EventArgs e)
        {
            if (!tangos.Any())
            {
                FastRibki.Stop();
                StaminaRibki.Stop();
            }

            int curstaminafish = -1;

            foreach (staminaf sf in staminafish)
            {
                //sf.fishsr = new TranslateTransform(sf.fx, sf.fy);
                double MinDistance2 = 1081.665;
                int curtango = -1;
                curstaminafish++;
                foreach (tango tango in tangos)
                {
                    curtango ++;
                    cur = Math.Sqrt(Math.Pow(sf.fx - tango.tx, 2) + Math.Pow(sf.fy - tango.ty, 2));
                    if (cur < MinDistance2)
                    {
                        MinDistance2 = cur;
                        tx = tango.tx;
                        ty = tango.ty;
                        tangostam = curtango;
                    }

                    if (sf.fx == tango.tx & sf.fy == tango.ty)
                    {
                        scene.Children.Remove(tango.tangofr);
                        sf.pregnant ++;
                    }
                }

                try
                {
                    if (sf.fx == tx & sf.fy == ty)
                    {
                        tangos.RemoveAt(tangostam);
                    }
                }
                catch(ArgumentOutOfRangeException)
                {

                }

                if (sf.fy == ty)//rl
                {
                    if (sf.fx < tx)//r
                    {
                        if (sf.fx != tx)
                        {
                            sf.fx += 50;
                        }
                    }

                    if (sf.fx > tx)//l
                    {
                        if (sf.fx != tx)
                        {
                            sf.fx -= 50;
                        }
                    }
                }

                if (sf.fx == tx)
                {
                    if (sf.fy < ty)//d
                    {
                        if (sf.fy != ty)
                        {
                            sf.fy += 50;
                        }
                    }

                    if (sf.fy > ty)//u
                    {
                        if (sf.fy != ty)
                        {
                            sf.fy -= 50;
                        }
                    }
                }

                if (sf.fx < tx) //r
                {
                    if (sf.fy < ty)//d
                    {
                        if (sf.fx - sf.fy < tx - ty)//rightdown
                        {
                            if (sf.fx != (tx - ty))
                            {
                                sf.fx += 50;
                            }

                            if ((sf.fx == (tx - ty))&&sf.fx != tx)
                            {
                                sf.fx += 50;
                                sf.fy += 50;
                            }
                        }

                        if (sf.fx - sf.fy > tx - ty)//downright
                        {
                            if (sf.fy != (ty - tx))
                            {
                                sf.fy += 50;
                            }

                            if ((sf.fy == (ty - tx)) &&sf.fy != ty)
                            {
                                sf.fx += 50;
                                sf.fy += 50;
                            }
                        }
                        if (sf.fx - sf.fy == tx - ty)//rddiag
                        {
                            if (sf.fy != ty)
                            {
                                sf.fx += 50;
                                sf.fy += 50;
                            }
                        }
                    }

                    //////////////////////////////////////////////

                    if (sf.fy > ty)//u
                    {
                        if (sf.fx + sf.fy < tx + ty)//rightup
                        {
                            if (sf.fx + sf.fy != (tx - ty))
                            {
                                sf.fx += 50;
                            }

                            if ((sf.fx + sf.fy == (tx - ty))&&sf.fx != tx)
                            {
                                sf.fx += 50;
                                sf.fy -= 50;
                            }
                        }

                        if (sf.fx + sf.fy > tx + ty)//upright
                        {
                            if (sf.fx + sf.fy != (ty - tx))
                            {
                                sf.fy -= 50;
                            }

                            if ((sf.fx + sf.fy == (ty - tx))&&sf.fy != ty)
                            {
                                sf.fx += 50;
                                sf.fy -= 50;
                            }
                        }
                        if (Math.Sqrt(Math.Pow(sf.fx + sf.fy, 2)) == Math.Sqrt(Math.Pow(tx + ty, 2)))//rdiag
                        {
                            if (sf.fy != ty)
                            {
                                sf.fx += 50;
                                sf.fy -= 50;
                            }
                        }
                    }
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

                if (sf.fx > tx) //l
                {
                    if (sf.fy < ty)//d
                    {
                        if (sf.fx + sf.fy > tx + ty)//leftdown
                        {
                            if (sf.fx + sf.fy != tx + ty)
                            {
                                sf.fx -= 50;
                            }

                            if ((sf.fx + sf.fy == tx + ty)&&sf.fx != tx)
                            {
                                sf.fx -= 50;
                                sf.fy += 50;
                            }
                        }

                        if (sf.fx + sf.fy < tx + ty)//downleft
                        {
                            if (sf.fx + sf.fy != ty + tx)
                            {
                                sf.fy += 50;
                            }

                            if ((sf.fx + sf.fy == ty + tx)&&sf.fy != ty)
                            {
                                sf.fx -= 50;
                                sf.fy += 50;
                            }
                        }
                        if (sf.fx + sf.fy == tx + ty)//ldiag
                        {
                            if (sf.fy != ty)
                            {
                                sf.fx -= 50;
                                sf.fy += 50;
                            }
                        }
                    }

                    //////////////////////////////////////////////

                    if (sf.fy > ty)//u
                    {
                        if (sf.fx - sf.fy < tx - ty)//leftup
                        {
                            if (sf.fx != (ty - tx))
                            {
                                sf.fx -= 50;
                            }

                            if ((sf.fx == (ty - tx))&&sf.fx != tx)
                            {
                                sf.fx -= 50;
                                sf.fy -= 50;
                            }
                        }

                        if (sf.fx - sf.fy > tx - ty)//upleft
                        {
                            if (sf.fy != (tx - ty))
                            {
                                sf.fy -= 50;
                            }

                            if ((sf.fy != (tx - ty))&&sf.fy != ty)
                            {
                                sf.fx -= 50;
                                sf.fy -= 50;
                            }
                        }

                        if (sf.fx - sf.fy == tx - ty)//rddiag
                        {
                            if (sf.fy != ty)
                            {
                                sf.fx -= 50;
                                sf.fy -= 50;
                            }
                        }
                    }
                }

                scene.Children.Remove(sf.fishsr);
                sf.fishsr.Margin = new Thickness(sf.fx, sf.fy, 0, 0);
                scene.Children.Add(sf.fishsr);

                //if (sf.fx == tx & sf.fy == ty)
                //{
                //    tangos.RemoveAt(tangostam);
                //    scene.Children.Remove(sf.tangofr);
                //}
            }
            
        }        

        //private void move(int fx,int fy, int tx, int ty)
        //{
        //    if (fy == ty)//rl
        //    {
        //        if (fx < tx)//r
        //        {
        //            if (fx != tx)
        //            {
        //                fx += 50;
        //            }
        //        }

        //        if (fx > tx)//l
        //        {
        //            if (fx != tx)
        //            {
        //                fx -= 50;
        //            }
        //        }
        //    }

        //    if (fx == tx)
        //    {
        //        if (fy < ty)//d
        //        {
        //            if (fy != ty)
        //            {
        //                fy += 50;
        //            }
        //        }

        //        if (fy > ty)//u
        //        {
        //            if (fy != ty)
        //            {
        //                fy -= 50;
        //            }
        //        }
        //    }

        //    if (fx < tx) //r
        //    {
        //        if (fy < ty)//d
        //        {
        //            if (fx - fy < tx - ty)//rightdown
        //            {
        //                if (fx != (tx - ty + fy))
        //                {
        //                    fx += 50;
        //                }

        //                if ((fx == (tx - ty + fy)) && fx != tx)
        //                {
        //                    fx += 50;
        //                    fy += 50;
        //                }
        //            }

        //            if (fx - fy > tx - ty)//downright
        //            {
        //                if (fy != (fx - tx + ty))
        //                {
        //                    fy += 50;
        //                }

        //                if ((fy != (fx - tx + ty)) && fy != ty)
        //                {
        //                    fx += 50;
        //                    fy += 50;
        //                }
        //            }
        //            if (fx - fy == tx - ty)//rddiag
        //            {
        //                if (fy != ty)
        //                {
        //                    fx += 50;
        //                    fy += 50;
        //                }
        //            }
        //        }

        //        //////////////////////////////////////////////

        //        if (fy > ty)//u
        //        {
        //            if (fx + fy < tx + ty)//rightup
        //            {
        //                if (fx + fy != (tx + ty - fy))
        //                {
        //                    fx += 50;
        //                }

        //                if ((fx + fy == (tx + ty - fy)) && fx != tx)
        //                {
        //                    fx += 50;
        //                    fy -= 50;
        //                }
        //            }

        //            if (fx + fy > tx + ty)//upright
        //            {
        //                if (fx + fy != (tx + ty - fx))
        //                {
        //                    fy -= 50;
        //                }

        //                if ((fx + fy == (tx + ty - fx)) && fy != ty)
        //                {
        //                    fx += 50;
        //                    fy -= 50;
        //                }
        //            }
        //            if (Math.Sqrt(Math.Pow(fx + fy, 2)) == Math.Sqrt(Math.Pow(tx + ty, 2)))//rdiag
        //            {
        //                if (fy != ty)
        //                {
        //                    fx += 50;
        //                    fy -= 50;
        //                }
        //            }
        //        }
        //    }

        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //    if (fx > tx) //l
        //    {
        //        if (fy < ty)//d
        //        {
        //            if (fx + fy > tx + ty)//leftdown
        //            {
        //                if (fx + fy != (tx + ty - fy))
        //                {
        //                    fx -= 50;
        //                }

        //                if ((fx + fy == (tx + ty - fy)) && fx != tx)
        //                {
        //                    fx -= 50;
        //                    fy += 50;
        //                }
        //            }

        //            if (fx + fy < tx + ty)//downleft
        //            {
        //                if (fx + fy != (tx + ty - fx))
        //                {
        //                    fy += 50;
        //                }

        //                if ((fx + fy == (tx + ty - fx)) && fy != ty)
        //                {
        //                    fx -= 50;
        //                    fy += 50;
        //                }
        //            }
        //            if (fx + fy == tx + ty)//ldiag
        //            {
        //                if (fy != ty)
        //                {
        //                    fx -= 50;
        //                    fy += 50;
        //                }
        //            }
        //        }

        //        //////////////////////////////////////////////

        //        if (fy > ty)//u
        //        {
        //            if (fx - fy < tx - ty)//leftup
        //            {
        //                if (fx != (tx - ty + fy))
        //                {
        //                    fx -= 50;
        //                }

        //                if ((fx == (tx - ty + fy)) && fx != tx)
        //                {
        //                    fx -= 50;
        //                    fy -= 50;
        //                }
        //            }

        //            if (fx - fy > tx - ty)//upleft
        //            {
        //                if (fx != (tx - ty + fy))
        //                {
        //                    fy -= 50;
        //                }

        //                if ((fx == (tx - ty + fy)) && fy != ty)
        //                {
        //                    fx += 50;
        //                    fy -= 50;
        //                }
        //            }
        //            if (fx - fy == tx - ty)//rddiag
        //            {
        //                if (fy != ty)
        //                {
        //                    fx -= 50;
        //                    fy -= 50;
        //                }
        //            }
        //        }
        //    }
        //}

    }
}

