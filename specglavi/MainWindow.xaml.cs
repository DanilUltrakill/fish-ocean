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
using System.Xml;
using System.Xml.Linq;
using OxyPlot;
using OxyPlot.Series;

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
        System.Windows.Threading.DispatcherTimer Rounding;
        System.Windows.Threading.DispatcherTimer Poihali;

        bool normal = true;
        ImageBrush ib = new ImageBrush();

        XmlDocument xDoc = new XmlDocument();
        MVM aga = new MVM();

        public MainWindow()
        {
            InitializeComponent();
            aga = (MVM)DataContext;
            //ib.AlignmentX = AlignmentX.Left;
            //ib.Stretch = Stretch.None;
            //ib.ViewboxUnits = BrushMappingMode.Absolute;
            ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/pictures/MAINMENU.jpg", UriKind.Absolute));
            mainfield.Background = ib;
            Normal.Visibility = Visibility.Hidden;
            Egoism.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Hidden;
            Fish1.Visibility = Visibility.Hidden;
            Fish2.Visibility = Visibility.Hidden;
            weeeeeed.Visibility = Visibility.Hidden;
            NextRound.Visibility = Visibility.Hidden;
        }

        int[,] fxy = new int[19, 13];        

        Random rnd = new Random();
        public int round;

        //public string Title { get; private set; }
        //public IList<DataPoint> fastf { get; private set; }
        //public IList<DataPoint> staminaf { get; private set; }
        //public IList<DataPoint> tango { get; private set; }

        //private void MVM()
        //{
        //    Title = "Fish ocean";
        //    fastf = new List<DataPoint>();
        //    staminaf = new List<DataPoint>();
        //    tango = new List<DataPoint>();


        //    XDocument xdoc = XDocument.Load("C:\\Users\\user\\source\\repos\\specglavi\\specglavi\\stats.xml");
        //    //XDocument xdoc = XDocument.Load("C:\\Users\\user\\source\\repos\\specglavi\\specglavi\\stats.xml");
        //    foreach (XElement elem in xdoc.Element("simulation").Elements("Round"))
        //    {
        //        XAttribute attrName = elem.Attribute("id");
        //        XElement ffcount = elem.Element("Fast_Fish");
        //        XElement sfcount = elem.Element("Stamina_Fish");
        //        double tcount = 10;

        //        if (attrName != null && ffcount != null && sfcount != null)
        //        {
        //            DataPoint point1 = new DataPoint(double.Parse(attrName.Value), double.Parse(ffcount.Value));
        //            DataPoint point2 = new DataPoint(double.Parse(attrName.Value), double.Parse(sfcount.Value));
        //            DataPoint point3 = new DataPoint(double.Parse(attrName.Value), tcount);
        //            fastf.Add(point1);
        //            staminaf.Add(point2);
        //            tango.Add(point3);
        //        }
        //    }
        //}

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
        private void NextRound_Click(object sender, RoutedEventArgs e)
        {
            StartProgram();
        }
        private void NextRound_Click1(object sender, RoutedEventArgs e)
        {
            StartProgram();
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Hidden;
            Start.Visibility = Visibility.Hidden;
            Options.Visibility = Visibility.Hidden;
            credits.Visibility = Visibility.Hidden;
            NextRound.Visibility = Visibility.Hidden;
            StartProgram();
        }
        private void StartProgram()
        {
            int fast = int.Parse(Fish1.Text);
            int stamina = int.Parse(Fish2.Text);
            int tango = int.Parse(weeeeeed.Text);

            NextRound.Visibility = Visibility.Hidden;
            chart.Visibility = Visibility.Hidden;

            ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/pictures/gggggggggggggggg.jpg", UriKind.Absolute));
            mainfield.Background = ib;

            if (round == 0)
            {
                fish_spawn(fast, stamina);
                tango_spawn(tango);

                xDoc.Load("C:\\Users\\user\\source\\repos\\specglavi\\specglavi\\stats.xml");
                // xDoc.Load("C:\\Users\\user\\source\\repos\\specglavi\\specglavi\\stats.xml");
                XmlElement xRoot = xDoc.DocumentElement;
                XmlElement OptionsElem = xDoc.CreateElement("Round");
                XmlAttribute numAttr = xDoc.CreateAttribute("id");

                XmlElement CYF = xDoc.CreateElement("Fast_Fish");
                XmlElement CPF = xDoc.CreateElement("Stamina_Fish");

                XmlText roundNum = xDoc.CreateTextNode(round.ToString());
                XmlText CYFn = xDoc.CreateTextNode(fastfish.Count().ToString());
                XmlText CPFn = xDoc.CreateTextNode(staminafish.Count().ToString());

                //Creating nodes
                numAttr.AppendChild(roundNum);
                CYF.AppendChild(CYFn);
                CPF.AppendChild(CPFn);
                OptionsElem.Attributes.Append(numAttr);
                OptionsElem.AppendChild(CYF);
                OptionsElem.AppendChild(CPF);
                xRoot.AppendChild(OptionsElem);
                //xDoc.Save("C:\\Users\\user\\source\\repos\\specglavi\\specglavi\\stats.xml");
                xDoc.Save("C:\\Users\\user\\source\\repos\\specglavi\\specglavi\\stats.xml");

            }
            else
            {
                //int f=0, s=0;
                foreach (fish fish in fish)
                {
                    int del = -1;
                    int cur = -1;
                    if (normal == true)
                    {
                        foreach (fastf ff in fastfish)
                        {
                            cur++;
                            if (ff.pregnant < 2)
                            {
                                del = cur;
                                scene.Children.Remove(ff.fishfr);
                            }
                        }
                        if (del != -1)
                        {
                            fastfish.RemoveAt(del);
                        }
                    }
                    if (normal == false)
                    {
                        foreach (fastf ff in fastfish)
                        {
                            if (ff.pregnant > 1)
                            {
                                foreach (fastf af in fastfish)
                                {
                                    if (af.pregnant == 0 && ff.pregnant>1)
                                    {
                                        af.pregnant++;
                                        ff.pregnant--;
                                    }
                                }
                            }
                        }
                        foreach (fastf ff in fastfish)
                        {
                            cur++;
                            if (ff.pregnant == 0)
                            {
                                del = cur;
                                scene.Children.Remove(ff.fishfr);
                            }
                        }
                        if (del != -1)
                        {
                            fastfish.RemoveAt(del);
                        }
                    }
                }
                int babyfish = 0;
                foreach (fish fish in fish)
                {
                    int del = -1;
                    int cur = -1;
                    babyfish = 0;
                    foreach (staminaf sf in staminafish)
                    {
                        cur++;
                        if (sf.pregnant == 0)
                        {
                            del = cur;
                            scene.Children.Remove(sf.fishsr);
                        }
                        if (normal == false)
                        {
                            if (sf.pregnant >= 2)
                            {
                                babyfish++;
                            }
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
                fish_spawn(0, babyfish);
                babyfish = 0;
                tango_spawn(tango);
            }

            round++;

            Rounding = new System.Windows.Threading.DispatcherTimer();
            Rounding.Tick += new EventHandler(Rounding_Tick);
            Rounding.Interval = new TimeSpan(0, 0, 0, 0, 400);

            FastRibki = new System.Windows.Threading.DispatcherTimer();
            FastRibki.Tick += new EventHandler(FastRibki_Tick);
            FastRibki.Interval = new TimeSpan(0, 0, 0, 0, 400);

            StaminaRibki = new System.Windows.Threading.DispatcherTimer();
            StaminaRibki.Tick += new EventHandler(StaminaRibki_Tick);
            StaminaRibki.Interval = new TimeSpan(0, 0, 0, 0, 800);

            Poihali = new System.Windows.Threading.DispatcherTimer();
            Poihali.Tick += new EventHandler(Poihali_Tick);
            Poihali.Interval = new TimeSpan(0, 0, 0, 0, 400);

            Rounding.Start();
            if (normal == true)
            {
                FastRibki.Start();
                StaminaRibki.Start();
            }
            else
                Poihali.Start();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/pictures/CHOOSEGAMEMODE.jpg", UriKind.Absolute));
            mainfield.Background = ib;
            Start.Visibility = Visibility.Hidden;
            Options.Visibility = Visibility.Hidden;
            credits.Visibility = Visibility.Hidden;
            Normal.Visibility = Visibility.Visible;
            Egoism.Visibility = Visibility.Visible;
        }

        int tx, ty;
        double cur;
        int tangofast, tangostam;

        private void Poihali_Tick(object sender, EventArgs e)
        {
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
                    if (cur < MinDistance1)
                    {
                        MinDistance1 = cur;
                        tx = tango.tx;
                        ty = tango.ty;
                        tangofast = curtango;
                    }

                    if (ff.fx == tango.tx & ff.fy == tango.ty)
                    {
                        scene.Children.Remove(tango.tangofr);
                        ff.pregnant++;
                    }
                }

                try
                {
                    if (ff.fx == tx & ff.fy == ty)
                    {
                        tangos.RemoveAt(tangofast);
                    }
                }
                catch (ArgumentOutOfRangeException)
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

                            if ((ff.fx + ff.fy == (tx + ty - ff.fy)) && ff.fx != tx)
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

                            if ((ff.fx + ff.fy == (tx + ty - ff.fx)) && ff.fy != ty)
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

                            if ((ff.fx + ff.fy == (tx + ty - ff.fy)) && ff.fx != tx)
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

                            if ((ff.fx + ff.fy == (tx + ty - ff.fx)) && ff.fy != ty)
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

                            if ((ff.fx == (tx - ty + ff.fy)) && ff.fx != tx)
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

                            if ((ff.fx == (tx - ty + ff.fy)) && ff.fy != ty)
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

            int curstaminafish = -1;

            foreach (staminaf sf in staminafish)
            {
                //sf.fishsr = new TranslateTransform(sf.fx, sf.fy);
                double MinDistance2 = 1081.665;
                int curtango = -1;
                curstaminafish++;
                foreach (tango tango in tangos)
                {
                    curtango++;
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
                        sf.pregnant++;
                    }
                }

                try
                {
                    if (sf.fx == tx & sf.fy == ty)
                    {
                        tangos.RemoveAt(tangostam);
                    }
                }
                catch (ArgumentOutOfRangeException)
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

                            if ((sf.fx == (tx - ty)) && sf.fx != tx)
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

                            if ((sf.fy == (ty - tx)) && sf.fy != ty)
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

                            if ((sf.fx + sf.fy == (tx - ty)) && sf.fx != tx)
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

                            if ((sf.fx + sf.fy == (ty - tx)) && sf.fy != ty)
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

                            if ((sf.fx + sf.fy == tx + ty) && sf.fx != tx)
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

                            if ((sf.fx + sf.fy == ty + tx) && sf.fy != ty)
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

                            if ((sf.fx == (ty - tx)) && sf.fx != tx)
                            {
                                sf.fx -= 50;
                                sf.fy -= 50;
                            }
                        }

                        if (sf.fx - sf.fy > tx - ty)//upleft
                        {
                            if (sf.fy != (tx - ty + sf.fy))
                            {
                                sf.fy -= 50;
                            }

                            if ((sf.fy != (tx - ty + sf.fy)) && sf.fy != ty)
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

        private void Rounding_Tick(object sender, EventArgs e)
        {
            if (!tangos.Any())
            {
                    FastRibki.Stop();
                    StaminaRibki.Stop();
                    Poihali.Stop();

                xDoc.Load("C:\\Users\\user\\source\\repos\\specglavi\\specglavi\\stats.xml");
                // xDoc.Load("C:\\Users\\user\\source\\repos\\specglavi\\specglavi\\stats.xml");
                XmlElement xRoot = xDoc.DocumentElement;
                XmlElement OptionsElem = xDoc.CreateElement("Round");
                XmlAttribute numAttr = xDoc.CreateAttribute("id");

                XmlElement CYF = xDoc.CreateElement("Fast_Fish");
                XmlElement CPF = xDoc.CreateElement("Stamina_Fish");

                XmlText roundNum = xDoc.CreateTextNode(round.ToString());
                XmlText CYFn = xDoc.CreateTextNode(fastfish.Count().ToString());
                XmlText CPFn = xDoc.CreateTextNode(staminafish.Count().ToString());

                //Creating nodes
                numAttr.AppendChild(roundNum);
                CYF.AppendChild(CYFn);
                CPF.AppendChild(CPFn);
                OptionsElem.Attributes.Append(numAttr);
                OptionsElem.AppendChild(CYF);
                OptionsElem.AppendChild(CPF);
                xRoot.AppendChild(OptionsElem);
                //xDoc.Save("C:\\Users\\user\\source\\repos\\specglavi\\specglavi\\stats.xml");
                xDoc.Save("C:\\Users\\user\\source\\repos\\specglavi\\specglavi\\stats.xml");

                Rounding.Stop();
                NextRound.Visibility = Visibility.Visible;

                if (round == 5)
                {
                    //painting();
                    //MVM();
                    //add_point();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Plot.InvalidatePlot(true);
                    });
                    aga.data();
                    chart.Visibility = Visibility.Visible;
                    NextRound.Visibility = Visibility.Hidden;
                }
            }
        }

        //private void create_chart()
        //{
        //    var minValue = DateTimeAxis.ToDouble(DateTime.Now.AddDays(-1));
        //    var maxValue = DateTimeAxis.ToDouble(DateTime.Now);
        //    model.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Minimum = minValue, Maximum = maxValue, StringFormat = "HH:mm" });
        //    var leftAxisY = new LinearAxis { Position = AxisPosition.Left };
        //    model.Axes.Add(leftAxisY);

        //    var lineSeries = new LineSeries
        //    {
        //        Title = "Линейный график",
        //        StrokeThickness = 3,
        //        LineStyle = LineStyle.Automatic,
        //        MarkerType = MarkerType.Circle,
        //        MarkerSize = 5,
        //        MarkerStroke = OxyColors.White,
        //        MarkerFill = OxyColors.Automatic,
        //        MarkerStrokeThickness = 1.5,
        //    };
        //    model.Series.Add(lineSeries);
        //    return model;
        //}

        private void add_point()
        {
            MVM graf = new MVM();
            XDocument xdoc = XDocument.Load("C:\\Users\\user\\source\\repos\\specglavi\\specglavi\\stats.xml");
            //XDocument xdoc = XDocument.Load("C:\\Users\\user\\source\\repos\\specglavi\\specglavi\\stats.xml");
            foreach (XElement elem in xdoc.Element("simulation").Elements("Round"))
            {
                XAttribute attrName = elem.Attribute("id");
                XElement ffcount = elem.Element("Fast_Fish");
                XElement sfcount = elem.Element("Stamina_Fish");
                double tcount = 10;

                if (attrName != null && ffcount != null && sfcount != null)
                {
                    DataPoint point1 = new DataPoint(double.Parse(attrName.Value), double.Parse(ffcount.Value));
                    DataPoint point2 = new DataPoint(double.Parse(attrName.Value), double.Parse(sfcount.Value));
                    DataPoint point3 = new DataPoint(double.Parse(attrName.Value), tcount);
                    graf.fastf.Add(point1);
                    graf.staminaf.Add(point2);
                    graf.tango.Add(point3);
                    Plot.InvalidatePlot();
                }
            }            
        }

        private void Normal_Click(object sender, RoutedEventArgs e)
        {
            ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/pictures/OPTIONSMENU.jpg", UriKind.Absolute));
            mainfield.Background = ib;
            Normal.Visibility = Visibility.Hidden;
            Egoism.Visibility = Visibility.Hidden;
            Fish1.Visibility = Visibility.Visible;
            Fish2.Visibility = Visibility.Visible;
            weeeeeed.Visibility = Visibility.Visible;
            Menu.Visibility = Visibility.Visible;
        }

        private void Egoism_Click(object sender, RoutedEventArgs e)
        {
            normal = false;
            ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/pictures/OPTIONSMENU2.jpg", UriKind.Absolute));
            mainfield.Background = ib;
            Normal.Visibility = Visibility.Hidden;
            Egoism.Visibility = Visibility.Hidden;
            Fish1.Visibility = Visibility.Visible;
            Fish2.Visibility = Visibility.Visible;
            weeeeeed.Visibility = Visibility.Visible;
            Menu.Visibility = Visibility.Visible;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/pictures/MAINMENU.jpg", UriKind.Absolute));
            mainfield.Background = ib;
            Start.Visibility = Visibility.Visible;
            Options.Visibility = Visibility.Visible;
            credits.Visibility = Visibility.Visible;
            Fish1.Visibility = Visibility.Hidden;
            Fish2.Visibility = Visibility.Hidden;
            weeeeeed.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Hidden;
        }

        private void FastRibki_Tick(object sender, EventArgs e)
        {
            //if(!tangos.Any())
            //{
            //    FastRibki.Stop();
            //    StaminaRibki.Stop();
            //    chart.Visibility = Visibility.Visible;
            //    mainfield.Visibility = Visibility.Hidden;
            //}

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
                    if (cur < MinDistance1)
                    {
                        MinDistance1 = cur;
                        tx = tango.tx;
                        ty = tango.ty;
                        tangofast = curtango;
                    }

                    if (ff.fx == tango.tx & ff.fy == tango.ty)
                    {
                        scene.Children.Remove(tango.tangofr);
                        ff.pregnant++;
                    }
                }

                try
                {
                    if (ff.fx == tx & ff.fy == ty)
                    {
                        tangos.RemoveAt(tangofast);
                    }
                }
                catch (ArgumentOutOfRangeException)
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

                            if ((ff.fx + ff.fy == (tx + ty - ff.fy)) && ff.fx != tx)
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

                            if ((ff.fx + ff.fy == (tx + ty - ff.fx)) && ff.fy != ty)
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

                            if ((ff.fx + ff.fy == (tx + ty - ff.fy)) && ff.fx != tx)
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

                            if ((ff.fx + ff.fy == (tx + ty - ff.fx)) && ff.fy != ty)
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

                            if ((ff.fx == (tx - ty + ff.fy)) && ff.fx != tx)
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

                            if ((ff.fx == (tx - ty + ff.fy)) && ff.fy != ty)
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
            //if (!tangos.Any())
            //{
            //    FastRibki.Stop();
            //    StaminaRibki.Stop();
            //}

            int curstaminafish = -1;

            foreach (staminaf sf in staminafish)
            {
                //sf.fishsr = new TranslateTransform(sf.fx, sf.fy);
                double MinDistance2 = 1081.665;
                int curtango = -1;
                curstaminafish++;
                foreach (tango tango in tangos)
                {
                    curtango++;
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
                        sf.pregnant++;
                    }
                }

                try
                {
                    if (sf.fx == tx & sf.fy == ty)
                    {
                        tangos.RemoveAt(tangostam);
                    }
                }
                catch (ArgumentOutOfRangeException)
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

                            if ((sf.fx == (tx - ty)) && sf.fx != tx)
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

                            if ((sf.fy == (ty - tx)) && sf.fy != ty)
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

                            if ((sf.fx + sf.fy == (tx - ty)) && sf.fx != tx)
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

                            if ((sf.fx + sf.fy == (ty - tx)) && sf.fy != ty)
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

                            if ((sf.fx + sf.fy == tx + ty) && sf.fx != tx)
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

                            if ((sf.fx + sf.fy == ty + tx) && sf.fy != ty)
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

                            if ((sf.fx == (ty - tx)) && sf.fx != tx)
                            {
                                sf.fx -= 50;
                                sf.fy -= 50;
                            }
                        }

                        if (sf.fx - sf.fy > tx - ty)//upleft
                        {
                            if (sf.fy != (tx - ty + sf.fy))
                            {
                                sf.fy -= 50;
                            }

                            if ((sf.fy != (tx - ty + sf.fy)) && sf.fy != ty)
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

    }
}

