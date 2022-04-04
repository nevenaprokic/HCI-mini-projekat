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

            
        }
        private void DrawHandler(object sender, RoutedEventArgs e)
        {
            string fromSymbol = comboFrom.SelectedValue.ToString().Substring(0, 3);
            string toSymbol = comboTo.SelectedValue.ToString().Substring(0, 3);
            string period = comboPeriod.SelectedValue.ToString();
            string attribute = comboAttribute.SelectedValue.ToString();
            string interval = "";

            Console.WriteLine(fromSymbol);
            Console.WriteLine(toSymbol);
            Console.WriteLine(period);
            Console.WriteLine(attribute);
            Console.WriteLine(interval);

            barChartData.createChart(fromSymbol, toSymbol, period, attribute);
            lineChartData.AddPair(period, fromSymbol, toSymbol, attribute, interval);

            DataContext = this;
        }
        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClickHandler(object sender, RoutedEventArgs e)
        {
            barChartData.cleanChart();
            lineChartData.cleanChart();
        }
        public LineChartData lineChartData { get; set; }
        public BarChartData barChartData { get; set; }
    }
}
