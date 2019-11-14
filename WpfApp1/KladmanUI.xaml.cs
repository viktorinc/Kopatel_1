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
using System.Windows.Threading;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for KladmanUI.xaml
    /// </summary>
    public partial class KladmanUI : Window
    {
        Test map1 = new Test();
        public KladmanViewModel usser { get; set; }
        public List<OrderViewModel> list = new List<OrderViewModel>();
        OrderViewModel order;
        public string Location;
        public KladmanUI( KladmanViewModel model)
        {

            InitializeComponent();
            Startup(model);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();

        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            Startup(usser);
        }


        private void Startup(KladmanViewModel model)
        {
            List<OrderViewModel> tmps = new List<OrderViewModel>() { };
            List<OrderViewModel> Final = new List<OrderViewModel>() { };

            HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:53306/api/order/orders/");
            request.Method = "GET";
            request.ContentType = "application/json";

            WebResponse response = request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string json = reader.ReadToEnd();
                var orders = JsonConvert.DeserializeObject<List<OrderViewModel>>(json);
                tmps = orders;

            }
            if (tmps.Count != list.Count)
            {
                foreach (var el in tmps)
                {
                    if (el.KladmenId == model.Id)
                    {
                        Final.Add(el);
                    }
                }
                list = Final;
                Orderslist.ItemsSource = list;
            }
            else { }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Test map_window = new Test();
            Startup(usser);
        }


        private void Orderslist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var senderList = (ListView)sender;
            order = list[Orderslist.SelectedIndex];
            map1.Closing += Closed;
            map1.Show();

           
            

        }


        public void Closed(object sender, EventArgs e)
        {
            string locationn = (sender as Test).GetLocation();
            Location = locationn;
            MessageBox.Show("DONE");
            order.Location = locationn;


            HttpWebRequest request = HttpWebRequest.CreateHttp($"http://localhost:53306/api/order/edit/{order.Id}");
            request.Method = "PUT";
            request.ContentType = "application/json";
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(order);

                writer.Write(json);
            }
                WebResponse response = request.GetResponse();
            



        }


    }
}
