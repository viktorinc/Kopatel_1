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
    /// Interaction logic for KladmanUI.xaml
    /// </summary>
    public partial class KladmanUI : Window
    {
        public KladmanViewModel usser { get; set; }
        public List<ProductViewModel> list = new List<ProductViewModel>();
        public KladmanUI()
        {

            InitializeComponent();
        }
    }
}
