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
using System.Windows.Controls;
using System.Windows.Media;
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

        public MainWindow()
        {
            InitializeComponent();
            List<string> currencyList = CSVReader.readCurrencyList();

            comboBox1.ItemsSource = currencyList;
            comboBox2.ItemsSource = currencyList;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;

            comboPeriod.ItemsSource = new[]{ "FX_INTRADAY", "FX_DAILY", "FX_WEEKLY", "FX_MONTHLY" };
            comboPeriod.SelectedIndex = 0;

            lineChartData = new LineChartData();
            barChartData = new BarChartData();

            DataContext = this;
        }
        public LineChartData lineChartData { get; set; }
        public BarChartData barChartData { get; set; }

        private void DrawHandler(object sender, RoutedEventArgs e)
        {
            string period = comboPeriod.Text;
            string fromSymbol = comboBox1.Text.Split('-')[0];
            string toSymbol = comboBox2.Text.Split('-')[0];
            string attribute = "1. open";
            string interval = "";
            lineChartData.AddPair(period, fromSymbol, toSymbol, interval, attribute);
        }
    }
}
