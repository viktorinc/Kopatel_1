using Microsoft.Maps.MapControl.WPF;
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
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        Pushpin pin;
        string location;
        public Test()
        {
            InitializeComponent();
        }
        public string GetLocation()
        {
            return pin.Location.ToString();
        }


        public void Map_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            Point mousePosition = e.GetPosition(this);
            Location pinLocation = Map.ViewportPointToLocation(mousePosition);

            pin = new Pushpin();
            pin.Location = pinLocation;

            Map.Children.Add(pin);
            MapLayer maplayer = new MapLayer();

            Map.Children.Add(maplayer);

            


        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Map.Children.Remove(pin);
        }
    }
}
