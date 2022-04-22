using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;

namespace HCI_mini_projekat
{
    public partial class BarChartData : UserControl
    {
        public int currency = 0;
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string[] color = { "#1D53A3", "#D66D6F", "#F3DB95", "#F39D5F", "#8F63A2", "#5F9479" };
        public BarChartData()
        {
            SeriesCollection = new SeriesCollection();
        }

        public void cleanChart()
        {
            currency = 0;
            SeriesCollection.Clear();
            Labels.Clear();
        }
        public void createChart(string fromSymbol, string toSymbol, string period, string attribute, string interval)
        {
            currency++;
            string function = "FX_" + period.ToUpper();
            string QUERY_URL = "";
            Console.WriteLine(interval);
            //https://www.alphavantage.co/query?function=FX_INTRADAY&from_symbol=EUR&to_symbol=USD&interval=5min&apikey=demo
            if (interval == "")
                QUERY_URL = "https://www.alphavantage.co/query?function=" + function + "&from_symbol=" + fromSymbol + "&to_symbol=" + toSymbol + "&apikey=JWLV0KC5UDNH6ODA";
            else
                QUERY_URL = "https://www.alphavantage.co/query?function=" + function + "&from_symbol=" + fromSymbol + "&to_symbol=" + toSymbol + "&interval=" + interval + "&apikey=JWLV0KC5UDNH6ODA";
            Console.WriteLine(QUERY_URL);
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(object));
                Console.WriteLine(json_data);
                List<Data> allData = ReadData.readData(json_data);

                string transaction = fromSymbol + "-" + toSymbol;
                drawChart(attribute, allData, transaction, period);
            }
        }

        private void drawChart(string attribute, List<Data> allData, string transaction, string period)
        {
            Labels = new List<string>();
            List<double> values = createValues(attribute, allData);

            SeriesCollection.Add(
                new ColumnSeries
                {
                    Title = transaction + " " + period,
                    Values = new ChartValues<double>(values),
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFrom(color[currency % 6])
                });


            for (int i = 0; i < 10; i++)
            {
                Labels.Add(allData[i].time);
            }
            YFormatter = value => value.ToString();
        }

        private List<double> createValues(string attribute, List<Data> allData)
        {

            List<double> values = new List<double>();

            for (int i = 0; i < 10; i++)
            {
                if (attribute.Equals("high"))
                    values.Add(allData[i].high);
                else if (attribute.Equals("low"))
                    values.Add(allData[i].low);
                else if (attribute.Equals("open"))
                    values.Add(allData[i].open);
                else if (attribute.Equals("close"))
                    values.Add(allData[i].close);
            }
            return values;
        }


    }

    public static class ReadData
    {
        public static List<Data> readData(dynamic json_data)
        {
            List<Data> allData = new List<Data>();
            var metaData = new Object();
            foreach (KeyValuePair<string, dynamic> kvp in json_data)
            {

                if (kvp.Key.Equals("Meta Data"))
                {
                    metaData = kvp.Value;
                }
                else
                {
                    metaData = kvp.Value;
                    foreach (KeyValuePair<string, dynamic> k in kvp.Value)
                    {
                        Data d = new Data();
                        d.time = k.Key;
                        foreach (KeyValuePair<string, dynamic> kp in k.Value)
                        {

                            if (kp.Key.Equals("1. open"))
                            {
                                d.open = Convert.ToDouble(kp.Value);
                            }
                            else if (kp.Key.Equals("2. high"))
                            {
                                d.high = Convert.ToDouble(kp.Value);
                            }
                            else if (kp.Key.Equals("3. low"))
                            {
                                d.low = Convert.ToDouble(kp.Value);
                            }
                            else if (kp.Key.Equals("4. close"))
                            {
                                d.close = Convert.ToDouble(kp.Value);
                            }
                        }
                        allData.Add(d);
                    }
                }
            }
            return allData;
        }
    }
   
    public class Data
    {
        public string time { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
    }
}
