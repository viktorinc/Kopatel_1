
using Microsoft.Maps.MapControl.WPF;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UserViewModel user { get; set; }
        public UserModel usser { get; set; }
        public List<KladmanViewModel> kladmans = new List<KladmanViewModel>() { };
        public List<ProductViewModel> list = new List<ProductViewModel>();
        public List<OrderViewModel> list2 = new List<OrderViewModel>();
        public OrderViewModel order = new OrderViewModel();
        
        public MainWindow(UserViewModel model)
        {
            user = model;
            InitializeComponent(); 
            Startup();

            MapStartup();
            ListProducts.ItemsSource = list;
        }

        public void MapStartup()
        {

            List<OrderViewModel> tmps = new List<OrderViewModel>() { };
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
            foreach (var el in tmps)
            {
                if (el.UserId == user.Id)
                {
                    list2.Add(el);
                }
            }
            
            foreach (var el in list2)
            {
                try { 
                string location = el.Location;
                string[] values = location.Split(',');
                double loc1 = Convert.ToDouble($"{values[0]},{values[1]}");
                double loc2 = Convert.ToDouble($"{values[2]},{values[3]}");
                Location pinLocation = new Location(loc1, loc2);
                Pushpin pin = new Pushpin();
                pin.Location = pinLocation;
                Map.Children.Add(pin);
                MapLayer maplayer = new MapLayer();
                Map.Children.Add(maplayer);
                 }
                catch 
                { //замовлення ще не виконав кладмен
                }


            }
        }

        private void Startup()
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:53306/api/product/products/");
            request.Method = "GET";
            request.ContentType = "application/json";

            WebResponse response = request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string json = reader.ReadToEnd();
                var products = JsonConvert.DeserializeObject<List<ProductViewModel>>(json);
                list = products;
                
            }
        }

        private void ListProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

                    var senderList = (ListView)sender;
                ProductViewModel product = list[senderList.SelectedIndex];
            
                DisplayProduct dp = new DisplayProduct(product,user);
                    dp.Show();
                
      
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OrderUi oui = new OrderUi(user);
            oui.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MapStartup();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }
    }
}
