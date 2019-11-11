using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for KladmanUI.xaml
    /// </summary>
    public partial class KladmanUI : Window
    {
        public KladmanViewModel usser { get; set; }
        public List<OrderViewModel> list = new List<OrderViewModel>();
        public string Location;
        public KladmanUI( KladmanViewModel model)
        {

            InitializeComponent();
            Startup(model);

        }

        private void Startup(KladmanViewModel model)
        {
            List<OrderViewModel> tmps = new List<OrderViewModel>() { };
            HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:49576/api/order/orders/");
            request.Method = "GET";
            request.ContentType = "application/json";

            WebResponse response = request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string json = reader.ReadToEnd();
                var orders = JsonConvert.DeserializeObject<List<OrderViewModel>>(json);
                tmps = orders;

            }
            foreach (var el in tmps)
            {
                if (el.KladmenId == model.Id)
                {
                    list.Add(el);
                }
            }
            Orderslist.ItemsSource = list;
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
