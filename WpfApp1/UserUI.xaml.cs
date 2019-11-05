
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
            using (StreamReader reader = new StreamReader(request.GetRequestStream()))
            {
                string json = reader.ReadToEnd();
                List<ProductViewModel> users = JsonConvert.DeserializeObject<List<ProductViewModel>>(json);
                list = users;
                WebResponse response = request.GetResponse();
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
