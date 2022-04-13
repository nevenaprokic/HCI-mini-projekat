﻿using System;
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
using LiveCharts;
using LiveCharts.Wpf;
using System.Data;
using CsvHelper;
using System.IO;

namespace HCI_mini_projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<string, string> tableAPIs{ get; set; }
        public LineChartData lineChartData { get; set; }
        public BarChartData barChartData { get; set; }
        public TableWindow tableWindow { get; set; }
        public List<string> currencyList;
        void SetProperties()
        {
            this.Title = "Exchange rate change";
            this.Height = 450;
            this.Width = 800;
            Uri iconUri = new Uri("../../images/bar-graph.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }
        public MainWindow()
        {
            
            InitializeComponent();
            SetProperties();
            
            currencyList = CSVReader.readCurrencyList();

            comboFrom.ItemsSource = currencyList;
            comboTo.ItemsSource = currencyList;

            comboPeriod.ItemsSource = new[] { "Intraday", "Daily", "Weekly", "Monthly" };
            comboPeriod.SelectedIndex = 0;

            comboInterval.ItemsSource = new[] { "1min", "5min", "15min", "30min", "60min" };
            comboInterval.SelectedIndex = 0;
            comboAttribute.ItemsSource = new[] { "low", "high", "open", "close" };
            comboAttribute.SelectedIndex = 0;

            lineChartData = new LineChartData();
            barChartData = new BarChartData();

            tableAPIs = new Dictionary<string, string>();

            
        }
        private void DrawHandler(object sender, RoutedEventArgs e)
        {
            
            if(comboFrom.SelectedValue != null && comboTo.SelectedValue != null)
            {
                
                string fromSymbol = comboFrom.SelectedValue.ToString().Substring(0, 3);
                string toSymbol = comboTo.SelectedValue.ToString().Substring(0, 3);
                string period = comboPeriod.SelectedValue.ToString();
                string attribute = comboAttribute.SelectedValue.ToString();
                string interval = "";
                if (comboPeriod.SelectedValue.ToString() == "Intraday")
                    interval = comboInterval.SelectedValue.ToString();


                barChartData.createChart(fromSymbol, toSymbol, period, attribute, interval);
                //lineChartData.AddPair(period, fromSymbol, toSymbol, attribute, interval);
                SetTableData(fromSymbol, toSymbol, period, attribute);
            }
            else
            {
                MessageWindow messageWindow = new MessageWindow("Entered currencies are incorrect!");
                messageWindow.Show();
            }
           

            DataContext = this;

            


        }
        private void drawComboboxInterval(object sender, SelectionChangedEventArgs e)
        {
            if (comboPeriod.SelectedItem != null)
            {
                
                string selectedCurrenciesPair = comboPeriod.SelectedValue.ToString();
                if (selectedCurrenciesPair.Equals("Intraday"))
                {
                    comboInterval.Visibility = Visibility.Visible;
                    labelInterval.Visibility = Visibility.Visible;
                }
                else
                {
                    comboInterval.Visibility = Visibility.Hidden;
                    labelInterval.Visibility = Visibility.Hidden;
                }
            }
        }
        private void ChangeHandler(object sender, RoutedEventArgs e)
        {
            if (comboPeriod.SelectedValue.ToString().Equals("Intraday"))
            {
                comboInterval.Visibility = Visibility.Visible;
            }
            else
            {
                comboInterval.Visibility = Visibility.Hidden;
            }
        }
       
        private void ClearkHandler(object sender, RoutedEventArgs e)
        {
            barChartData.cleanChart();
            lineChartData.cleanChart();
            tableAPIs.Clear();
        }

        private void ViewTableBtn(object sender, RoutedEventArgs e)
        {
            tableWindow = new TableWindow();

            tableWindow.Show();

        }
        private void SetTableData(string fromSymbol, string toSymbol, string value3, string value4)
        {
            string function = "FX_" + value3.ToUpper();

            string QUERY_URL = "https://www.alphavantage.co/query?function=" + function + "&from_symbol=" + fromSymbol + "&to_symbol=" + toSymbol + "&apikey=JWLV0KC5UDNH6ODA";
           
            //ovde treba da se pozove funkcija za formiranje API-a i dobijeni api se ubaci u tableAPIs
            //string api1 = "https://www.alphavantage.co/query?function=FX_INTRADAY&from_symbol=EUR&to_symbol=USD&interval=5min&apikey=demo";
            //string api2 = "https://www.alphavantage.co/query?function=FX_DAILY&from_symbol=EUR&to_symbol=USD&apikey=demo";
            //string tittle1 = "EUR-USD1"; //title moze da se uzme od selektovanih elemenata combobox-a
            string tittle = fromSymbol + "-" + toSymbol; //da li na table view dugme da se i uzimaju podaci ili da zavisi od draw buttona

            if (tableAPIs.ContainsKey(tittle))
            {
                tableAPIs[tittle] = QUERY_URL;
            }
            else
            {
                tableAPIs.Add(tittle, QUERY_URL);
            }
            if(tableWindow != null)
            {
                tableWindow.addToComboBox(tittle, QUERY_URL);
            }
            
        }
    }

}
