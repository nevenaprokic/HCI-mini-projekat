using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

//using System.Web.Script.Serialization;



namespace HCI_mini_projekat
{
    internal class CurrencyData
    {

        public dynamic getData(string url)
        {
            Uri queryUri = new Uri(url);

            using (WebClient client = new WebClient())
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(object));

                return json_data;

            }
        }

    }

}