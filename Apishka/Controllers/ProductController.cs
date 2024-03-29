﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1;
using Kopatel.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kopatel.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EFContext _context;
        public ProductController(EFContext context)
        {
            _context = context;
        }

        [HttpGet("products")]
        public IEnumerable<ProductViewModel> Get()
        {
            return _context.Products.
                Select(t=>
                new ProductViewModel()
                {
                    Name = t.Name,
                    Price=t.Price,
                    Description=t.Description,
                    Picture=t.Picture,
                    Id = t.Id
                }).ToList();
        }

        // GET api/client/5
        [HttpGet("{id}")]
        public ContentResult GetProduct(int id)
        {
            Product user = _context.Products.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return Content("NotFound!");
            string json = JsonConvert.SerializeObject(user);

            return Content(json, "application/json");
        }

        // POST api/client
        [HttpPost("add")]
        public ContentResult AddProduct([FromBody]ProductViewModel model)
        {
            try
            {
                Product client = new Product()
                {
                   Name=model.Name,
                   Price=model.Price,
                   Description=model.Description,
                   Picture=model.Picture,
                   Id = model.Id
                };

                _context.Products.Add(client);
                _context.SaveChanges();

                return Content("Added Product");
            }
            catch (Exception ex)
            {
                return Content("Error:" + ex.Message);
            }
        }

        // PUT api/client/5
        [HttpPut("edit/{id}")]
        public ContentResult EditKladman(int id, [FromBody]ProductViewModel model)
        {
            var Kladman = _context.Products.FirstOrDefault(t => t.Id == id);
            if (Kladman != null)
            {
                Kladman.Price = model.Price;
                Kladman.Name = model.Name;
                Kladman.Picture = model.Picture;
                Kladman.Description = model.Description;
                Kladman.Id = model.Id;


                _context.SaveChanges();
                return Content("Product edited!");
            }
            return Content("Product data = null!");
        }

        // DELETE api/client/delete
        [HttpDelete("delete/{id}")]
        public ContentResult DeleteProduct(int id)
        {
            var Product = _context.Products.FirstOrDefault(t => t.Id == id);
            if (Product != null)
            {
                _context.Products.Remove(Product);
                _context.SaveChanges();
                return Content("Product deleted!");
            }
            return Content("Product" + " not found!");
        }


    }
}