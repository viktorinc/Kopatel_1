﻿using System;
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
        public string picturepath;
        public DisplayProduct()
        {
            //picturepath = product.Picture;
            InitializeComponent();
        }
    }
}
