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

        public MainWindow()
        {
            InitializeComponent();
            List<string> currencyList = CSVReader.readCurrencyList();

            comboFrom.ItemsSource = currencyList;
            comboTo.ItemsSource = currencyList;

            comboPeriod.ItemsSource = new[] { "Intraday", "Daily", "Weekly", "Monthly" };
            comboPeriod.SelectedIndex = 0;

            comboInterval.ItemsSource = new[] { "1min", "5min", "15min", "30min", "60min" };
            comboAttribute.ItemsSource = new[] { "low", "high", "open", "close" };
            comboAttribute.SelectedIndex = 0;

            lineChartData = new LineChartData();
            barChartData = new BarChartData();

            tableAPIs = new Dictionary<string, string>();


        }
        private void DrawHandler(object sender, RoutedEventArgs e)
        {
            string fromSymbol = comboFrom.SelectedValue.ToString().Substring(0, 3);
            string toSymbol = comboTo.SelectedValue.ToString().Substring(0, 3);
            string period = comboPeriod.SelectedValue.ToString();
            string attribute = comboAttribute.SelectedValue.ToString();
            string interval = "";

            barChartData.createChart(fromSymbol, toSymbol, period, attribute);
            lineChartData.AddPair(period, fromSymbol, toSymbol, attribute, interval);
            SetTableData(fromSymbol, toSymbol, period, attribute);

            DataContext = this;

            


        }
        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {

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
