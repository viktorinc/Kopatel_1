﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class OrderViewModel
    {
        public string Location { get; set; }

        public int KladmenId { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }
    }
}
