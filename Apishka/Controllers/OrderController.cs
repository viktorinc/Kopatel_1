using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apishka.Models;
using ClassLibrary1;
using Kopatel.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kopatel.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly EFContext _context;
        public OrderController(EFContext context)
        {
            _context = context;
        }

        [HttpGet("orders")]
        public IEnumerable<Order> Get()
        {
            return _context.Orders.ToList();
        }

        // GET api/client/5
        [HttpGet("{id}")]
        public ContentResult GetOrder(int id)
        {
            Order user = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return Content("NotFound!");
            string json = JsonConvert.SerializeObject(user);

            return Content(json, "application/json");
        }

        // POST api/client
        [HttpPost("add")]
        public ContentResult AddOrder([FromBody]OrderViewModel model)
        {
            try
            {
                Order client = new Order()
                {
                   UserId=model.UserId,
                   Location=model.Location,
                   ProductId=model.ProductId,KladmenId=model.KladmenId
                };

                _context.Orders.Add(client);
                _context.SaveChanges();

                return Content("Added Order");
            }
            catch (Exception ex)
            {
                return Content("Error:" + ex.Message);
            }
        }

        // PUT api/client/5
        [HttpPut("edit/{id}")]
        public ContentResult EditKladman(int id, [FromBody]OrderViewModel model)
        {
            var Kladman = _context.Orders.FirstOrDefault(t => t.Id == id);
            if (Kladman != null)
            {
                Kladman.KladmenId = model.KladmenId;
                Kladman.Location = model.Location;
                Kladman.UserId = model.UserId;
                Kladman.ProductId = model.ProductId;


                _context.SaveChanges();
                return Content("Order edited!");
            }
            return Content("Order data = null!");
        }

        // DELETE api/client/delete
        [HttpDelete("delete/{id}")]
        public ContentResult DeleteOrder(int id)
        {
            var Product = _context.Orders.FirstOrDefault(t => t.Id == id);
            if (Product != null)
            {
                _context.Orders.Remove(Product);
                _context.SaveChanges();
                return Content("Order deleted!");
            }
            return Content("Order" + " not found!");
        }


    }
}