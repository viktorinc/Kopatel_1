using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public int KladmenId { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public override string ToString()
        {
            ProductViewModel product = new ProductViewModel();
            List<ProductViewModel> tmps = new List<ProductViewModel>() { };
            HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:53306/api/product/products/");
            request.Method = "GET";
            request.ContentType = "application/json";

            WebResponse response = request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string json = reader.ReadToEnd();
                var products = JsonConvert.DeserializeObject<List<ProductViewModel>>(json);
                tmps = products;

            }
            foreach(var el in tmps)
            {
                if(el.Id==ProductId)
                {
                    product = el;
                }
            }
            if (Location==null)
            {
                Location = "Waiting for Kladman";
            }
            return $"{product.Name} Price:{product.Price} Location: {Location}";
        }
    }
}
