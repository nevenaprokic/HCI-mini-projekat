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


            lineChartData = new LineChartData();
            barChartData = new BarChartData();

            DataContext = this;
        }
        public LineChartData lineChartData { get; set; }
        public BarChartData barChartData { get; set; }
    }
}
