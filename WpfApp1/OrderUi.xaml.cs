﻿using Newtonsoft.Json;
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
    /// Interaction logic for OrderUi.xaml
    /// </summary>
    public partial class OrderUi : Window
    {

        UserViewModel user { get; set; }


        List<OrderViewModel> orders = new List<OrderViewModel>() { };


        public OrderUi(UserViewModel model)
        {
            user = model;
            InitializeComponent();
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
            foreach(var el in tmps)
            {
                if(el.UserId==model.Id)
                {
                    orders.Add(el);
                }
            }
            Orders.ItemsSource = orders;
        }

    }
}
