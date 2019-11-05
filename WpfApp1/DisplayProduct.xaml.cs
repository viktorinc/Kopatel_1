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
        public DisplayProduct(ProductViewModel model)
        {
          
            InitializeComponent();
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
    }
}
