
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
        public OrderViewModel order = new OrderViewModel();
        
        public MainWindow()
        {
            InitializeComponent();
            Startup();
            ListProducts.ItemsSource = list;
        }

        private void Startup()
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:49576/api/product/products/");
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

        private void ListProducts_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OrderUi oui = new OrderUi(user);
            oui.Show();
        }

        private void Map_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            Point mousePosition = e.GetPosition(this);
            Location pinLocation = Map.ViewportPointToLocation(mousePosition);

            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;

            Map.Children.Add(pin);
            MapLayer maplayer = new MapLayer();
         
            Map.Children.Add(maplayer);
          

        }

    }
}
