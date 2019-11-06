using System;
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

        public override string ToString()
        {
            if(Location==null)
            {
                Location = "Waiting for Kladman";
            }
            return $"Product id: {ProductId} Location: {Location}";
        }
    }
}
