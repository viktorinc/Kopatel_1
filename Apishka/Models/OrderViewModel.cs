using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apishka.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Location { get; set; }

        public int KladmenId { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }
    }
}
