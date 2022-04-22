﻿using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Controls;
<<<<<<< HEAD
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
=======
>>>>>>> 349ab915a31256f31ff3b793ee74fe876425a0ee

namespace HCI_mini_projekat
{
    public partial class LineChartData : UserControl
    {
        public LineChartData()
        {
<<<<<<< HEAD
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
=======
            AttributeMapper = new Dictionary<string, string>
            {
                { "open", "1. open" },
                { "high", "2. high" },
                { "low", "3. low" },
                { "close", "4. close" }
            };

            TimeSeriesMapper = new Dictionary<string, string>
            {
                { "Intraday", "FX_INTRADAY"},
                { "Daily", "FX_DAILY" },
                { "Weekly", "FX_WEEKLY" },
                { "Monthly", "FX_MONTHLY" }
            };

            SeriesCollection = new SeriesCollection { };

            Labels = new List<string>();
            YFormatter = value => value.ToString();

            DataContext = this;
        }

        public void AddPair(string timeSeries, string fromSymbol, string toSymbol, string attribute, string interval)
        {
            // api key W1M42UWZUELKQJII
            try
            {
                string QUERY_URL = null;

                if (interval != "")
                {
                    QUERY_URL = $"https://www.alphavantage.co/query?function={TimeSeriesMapper[timeSeries]}&from_symbol={fromSymbol}&to_symbol={toSymbol}&interval={interval}&apikey=W1M42UWZUELKQJII";
                }
                else
                {
                    QUERY_URL = $"https://www.alphavantage.co/query?function={TimeSeriesMapper[timeSeries]}&from_symbol={fromSymbol}&to_symbol={toSymbol}&apikey=W1M42UWZUELKQJII";
                }
                Uri queryUri = new Uri(QUERY_URL);

                using (WebClient client = new WebClient())
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(object));
                    Dictionary<string, dynamic>.KeyCollection keys = json_data.Keys;
                    // Key : "Time Series FX (5min)"
                    // Next Key: "1. open" 
                    string key = computeKey(timeSeries, interval);
                    
                    //printParameters(fromSymbol, toSymbol, timeSeries, interval, attribute, QUERY_URL);
                    Console.WriteLine(json_data);
                    Console.WriteLine("Kljuc: ", key);
                    dynamic list = json_data[$"Time Series FX ({key})"];
                    ChartValues<double> values = new ChartValues<double>();

                    foreach (KeyValuePair<string, dynamic> entry in list)
                    {
                        double val = Double.Parse(entry.Value[AttributeMapper[attribute]]);
                        Labels.Add(entry.Key);
                        values.Add(val);
                    }
                    LineSeries lineSeries = new LineSeries
                    {
                        Title = $"{fromSymbol}-{toSymbol} {timeSeries}",
                        Values = values,
                        PointGeometry = null,
                        PointGeometrySize = 15
                    };
                    SeriesCollection.Add(lineSeries);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal void cleanChart()
        {
            SeriesCollection.Clear();
            Labels.Clear();
        }

        private void printParameters(string fromSymbol, string toSymbol, string timeSeries, string interval, string attribute, string QUERY_URL)
        {
            Console.WriteLine("FROM:" + fromSymbol);
            Console.WriteLine("TO:" + toSymbol);
            Console.WriteLine("TIME_SERIES:" + timeSeries);
            Console.WriteLine("INTERVAL:" + interval);
            Console.WriteLine("ATTRIBUTE:" + attribute);
            Console.WriteLine("URL:" + QUERY_URL);
>>>>>>> 349ab915a31256f31ff3b793ee74fe876425a0ee
        }

        private string computeKey(string timeSeries, string interval)
        {
<<<<<<< HEAD
            if (interval == "") return TimeSeriesMapper[timeSeries];
=======
            if (interval == "") return timeSeries;
>>>>>>> 349ab915a31256f31ff3b793ee74fe876425a0ee
            return interval;
        }

        public SeriesCollection SeriesCollection { get; set; }
<<<<<<< HEAD
        private Dictionary<string, string> TimeSeriesMapper { get; set; }
        public string[] Labels { get; set; }
=======
        private Dictionary<string, string> AttributeMapper { get; set; }
        private Dictionary<string, string> TimeSeriesMapper { get; set; }
        public List<string> Labels { get; set; }
>>>>>>> 349ab915a31256f31ff3b793ee74fe876425a0ee
        public Func<double, string> YFormatter { get; set; }

    }
}
