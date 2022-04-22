using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;

namespace HCI_mini_projekat
{
    public partial class LineChartData : UserControl
    {
        public LineChartData()
        {
            TimeSeriesMapper = new Dictionary<string, string>
            {
                { "FX_DAILY", "Daily" },
                { "FX_WEEKLY", "Weekly" },
                { "FX_MONTHLY", "Monthly" }
            };

            SeriesCollection = new SeriesCollection{};

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");

            DataContext = this;
        }

        public void AddPair(string timeSeries, string from_symbol, string to_symbol, string interval="", string attribute="")
        {
            // api key W1M42UWZUELKQJII
            string QUERY_URL = $"https://www.alphavantage.co/query?function={timeSeries}&from_symbol={from_symbol}&to_symbol={to_symbol}&interval={interval}&apikey=W1M42UWZUELKQJII";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(object));
                Dictionary<string, dynamic>.KeyCollection keys = json_data.Keys;
                // Key : "Time Series FX (5min)"
                // Next Key: "1. open" 
                string key = computeKey(timeSeries, interval);
                Console.WriteLine("KLJUC _____" + key);
                dynamic list = json_data[$"Time Series FX ({key})"];
                ChartValues<double> values = new ChartValues<double>();
                
                foreach (KeyValuePair<string, dynamic> entry in list)
                {
                    double val = Double.Parse(entry.Value[attribute]);
                    values.Add(val);
                }
                LineSeries lineSeries = new LineSeries
                {
                    Title = $"{from_symbol}-{to_symbol}",
                    Values = values,
                    PointGeometry = null,
                    PointGeometrySize = 15
                };
                SeriesCollection.Add(lineSeries);
            }
        }

        private string computeKey(string timeSeries, string interval)
        {
            if (interval == "") return TimeSeriesMapper[timeSeries];
            return interval;
        }

        public SeriesCollection SeriesCollection { get; set; }
        private Dictionary<string, string> TimeSeriesMapper { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

    }
}
