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

        public MVM()
        {
            this.Title = "Fish ocean";
            this.fastf = new List<DataPoint>();
            this.staminaf = new List<DataPoint>();

            //XDocument xdoc = XDocument.Load("C:\\Users\\Vladimir\\Desktop\\FnS\\Statistics.xml");
            XDocument xdoc = XDocument.Load("C:\\Users\\user\\source\\repos\\specglavi\\specglavi\\stats.xml");
            foreach (XElement elem in xdoc.Element("simulation").Elements("Round"))
            {
                XAttribute attrName = elem.Attribute("id");
                XElement ffcount = elem.Element("Fast_Fish");
                XElement sfcount = elem.Element("Stamina_Fish");

                if (attrName != null && ffcount != null && sfcount != null)
                {
                    DataPoint point1 = new DataPoint(double.Parse(attrName.Value), double.Parse(ffcount.Value));
                    DataPoint point2 = new DataPoint(double.Parse(attrName.Value), double.Parse(sfcount.Value));
                    fastf.Add(point1);
                    staminaf.Add(point2);
                }
            }
        }

    }
}
