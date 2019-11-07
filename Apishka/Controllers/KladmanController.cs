using System;
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
    [Route("api/kladman")]
    [ApiController]
    public class KladmanController : ControllerBase
    {
        private readonly EFContext _context;
        public KladmanController(EFContext context)
        {
            _context = context;
        }

        [HttpGet("kladmans")]
        public IEnumerable<KladmanViewModel> Get()
        {
            return _context.Kladmans.Select(t =>
            new KladmanViewModel()
            {
                Id = t.Id,
                IsAlive = t.IsAlive,
                Login = t.Login,
                Password=t.Password,
                Rating=t.Rating

            });
        }

        [HttpPost("loginKladman")]
        public IActionResult CheckLogin([FromBody]KladmanViewModel model)
        {
            var boss = _context.Kladmans.FirstOrDefault(t => t.Login == model.Login  && t.Password == model.Password);
            if (boss != null)
            {
                return Ok(boss.Id.ToString());
            }
            else
            {
                return BadRequest();
            }
        }

        // GET api/client/5
        [HttpGet("{id}")]
        public ContentResult GetKladman(int id)
        {
            Kladman user = _context.Kladmans.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return Content("NotFound!");
            string json = JsonConvert.SerializeObject(user);

            return Content(json, "application/json");
        }

        // POST api/client
        [HttpPost("add")]
        public ContentResult AddKladman([FromBody]KladmanViewModel model)
        {
            try
            {
                Kladman client = new Kladman()
                {
                    IsAlive = model.IsAlive,
                    Password = model.Password,
                    Login = model.Login,
                    Rating = model.Rating,
                    Id=model.Id
                };

                _context.Kladmans.Add(client);
                _context.SaveChanges();

                return Content("Added kladman");
            }
            catch (Exception ex)
            {
                return Content("Error:" + ex.Message);
            }
        }

        // PUT api/client/5
        [HttpPut("edit/{id}")]
        public ContentResult EditKladman(int id, [FromBody]KladmanViewModel model)
        {
            var Kladman = _context.Kladmans.FirstOrDefault(t => t.Id == id);
            if (Kladman != null)
            {
                Kladman.Rating = model.Rating;
                Kladman.Login = model.Login;
                Kladman.Password = model.Password;
                Kladman.IsAlive = model.IsAlive;
                Kladman.Id = model.Id;
                

                _context.SaveChanges();
                return Content("Kladman edited!");
            }
            return Content("Klaman data = null!");
        }

        // DELETE api/client/delete
        [HttpDelete("delete/{id}")]
        public ContentResult DeleteKladman(int id)
        {
            var client = _context.Kladmans.FirstOrDefault(t => t.Id == id);
            if (client != null)
            {
                _context.Kladmans.Remove(client);
                _context.SaveChanges();
                return Content("Kladman deleted!");
            }
            return Content("Kladman" +" not found!");
        }


    }
}