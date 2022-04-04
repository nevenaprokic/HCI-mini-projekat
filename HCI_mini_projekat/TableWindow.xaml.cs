using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace HCI_mini_projekat
{
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public class CurrencyRowData
    {
        public string time { get; set; }
        public string open { get; set; }
        public string low { get; set; }
        public string heigh { get; set; }
        public string close { get; set; }

        public CurrencyRowData(string time, string open, string low, string heigh, string close)
        {
            this.time = time;
            this.open = open;
            this.low = low;
            this.heigh = heigh;
            this.close = close;
        }
    }
    public partial class TableWindow : Window
    {
        public string[] color = { "#1D53A3", "#D66D6F", "#F3DB95", "#F39D5F", "#8F63A2", "#5F9479" };
        public string tableTittle { get; set; }
        public Dictionary<String, Object> tableData { get; set; }

        public ObservableCollection<CurrencyRowData> tableRowsData { get; set; }

        public string firstCurrency { get; set; }
        public string secondCurrency { get; set; }

        Dictionary<string, string> tableAPIs { get; set; }

        CurrencyData data { get; set; }

        public TableWindow()
        {
            InitializeComponent();
            DataContext = this;

            var window = Application.Current.MainWindow; //referenca na main window

            tableRowsData = new ObservableCollection<CurrencyRowData>();
            data = new CurrencyData();

            tableAPIs = (window as MainWindow).tableAPIs;
            //preuyeti kljuc da bude naziv buttona, automatski otvoriti prvo dugme, a ostalo pozivati po kliku, na klik uzeti ime buttona i pristuptiti mapi, uzeti api i iscrtati
            createComboBox();

            //popuniti podacima


            //drawTable(tableAPIs.ElementAt(0).Value);

            

            //string cardTitle = firstCurrency.Substring(0, 3) + "-" +secondCurrency.Substring(0,3);



        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void createComboBox()
        {
           
            foreach (var elem in tableAPIs)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = elem.Key;
                currenciesCombobox.Items.Add(item);
            }

            currenciesCombobox.SelectedItem = currenciesCombobox.Items[0];
        }
        private void currrencyComboboxChanged(object sender, SelectionChangedEventArgs e)
        {
            if (currenciesCombobox.SelectedItem != null)
            {
                //ComboBoxItem cbi1 = (ComboBoxItem)(sender as ComboBox).SelectedItem;
                ComboBoxItem item = (ComboBoxItem)currenciesCombobox.SelectedItem;
                string selectedCurrenciesPair = item.Content.ToString();
                string api = tableAPIs[selectedCurrenciesPair];
                drawTable(api);
            }
        }

        private void drawTable(string api)
        {

            tableRowsData.Clear(); //da se prvo isprazne prethodni podaci

            dynamic json_data = data.getData(api); //ovde prosledjivati potrebne parametre pa prilagoditi tamo api

            var dictionary = (Dictionary<string, object>)json_data;
            var dataSet = dictionary.ElementAt(1);
            Dictionary<String, Object> listData = (Dictionary<String, Object>)dataSet.Value;
            tableTittle = dataSet.Key.ToString();

            foreach (var elem in listData)
            {
                Dictionary<string, object> valueData = (Dictionary<string, object>)elem.Value;
                CurrencyRowData rowData = new CurrencyRowData(elem.Key.ToString(), valueData["1. open"].ToString(), valueData["3. low"].ToString(), valueData["2. high"].ToString(), valueData["4. close"].ToString()
                    );

                tableRowsData.Add(rowData);
            }
            tableData = listData;

            NotifyPropertyChanged("currencyData");

        }

        public void addToComboBox(string tittle, string api)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = tittle;
            currenciesCombobox.Items.Add(item);
        }
    }
}
