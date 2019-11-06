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
    /// Interaction logic for DisplayProduct.xaml
    /// </summary>
    public partial class DisplayProduct : Window
    {
        public ProductViewModel product { get; set; }
        public UserViewModel user { get; set; }
        public DisplayProduct(ProductViewModel model,UserViewModel umodel )
        {
          
            InitializeComponent();
            user = umodel;
            product = model;

            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri($@"C:\Users\kubrak\Source\Repos\Kopatel_1\WpfApp1\Pics\{model.Picture}");
            myBitmapImage.EndInit();

            Desc.Text = model.ForDisplay();
            productpic.Source = myBitmapImage;
            string picturepath = model.Picture;
            
        }
        private void startup()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OrderViewModel order = new OrderViewModel();
            order.UserId = user.Id;
            order.ProductId = product.Id;
            order.KladmenId = 1;
            order.Location = "Waiting for Kladman";
            HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:49576/api/order/add/");
            request.Method = "POST";
            request.ContentType = "application/json";
            using (StreamWriter stream = new StreamWriter(request.GetRequestStream()))
            {
                order = new OrderViewModel()
                {
                   KladmenId = order.KladmenId,
                   ProductId=order.ProductId,
                   UserId=order.ProductId,
                   Location = order.Location
                };
                string json = JsonConvert.SerializeObject(order);
                stream.Write(json);


            }
            WebResponse response = request.GetResponse();
            MessageBox.Show("Your order made succesfully. Wait for a location, you can check it in the page <My orders>");
        }
    }
}
