using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OxyPlot;

namespace specglavi
{
    class MVM
    {
        public string Title { get; private set; }
        public IList<DataPoint> fastf { get; private set; }
        public IList<DataPoint> staminaf { get; private set; }
        public IList<DataPoint> tango { get; private set; }

        public MVM()
        {
            Title = "Fish ocean";
            fastf = new List<DataPoint>();
            staminaf = new List<DataPoint>();
            tango = new List<DataPoint>();


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
                    fastf.Add(point1);
                    staminaf.Add(point2);
                    tango.Add(point3);
                }
            }
        }

    }
}
