using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_mini_projekat
{
    static class CSVReader
    {
        public static List<string> readCurrencyList()
        {
            List<string> currencyList = new List<string>();
            string path = @"./../../physical_currency_list.csv";

            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines.Skip(1))
            {
                string[] columns = line.Split(',');
                currencyList.Add(columns[0] + "-" + columns[1]);
            }
           
            return currencyList;
        }
    }
}
