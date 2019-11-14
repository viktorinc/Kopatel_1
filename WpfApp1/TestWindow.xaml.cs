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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
            string location = "50,6305651448626,26,246676265625,0";
                string[] values = location.Split(',');
            double loc1 = Convert.ToDouble($"{values[0]},{values[1]}");
            double loc2 = Convert.ToDouble($"{values[2]},{values[3]}");
            Location pinLocation = new Location(loc1,loc2);
            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;
            Map.Children.Add(pin);
            MapLayer maplayer = new MapLayer();
            Map.Children.Add(maplayer);

        }
    }
}
