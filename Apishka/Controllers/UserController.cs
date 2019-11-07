using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1;
using Kopatel.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WpfApp1.Models;

namespace Kopatel.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly EFContext _context;
        public UserController(EFContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        public IEnumerable<User> Get()
        {
            return _context.Users.ToList();
        }

        // GET api/client/5
        [HttpGet("{id}")]
        public ContentResult GetClient(int id)
        {
            User user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return Content("NotFound!");
            string json = JsonConvert.SerializeObject(user);

            return Content(json, "application/json");
        }

        // POST api/client
        [HttpPost("add")]
        public ContentResult AddClient([FromBody]UserViewModel model)
        {
            try
            {
                User client = new User()
                {
                    Login = model.Login,
                    Password=model.Password,
                    Id = model.Id
                };

                _context.Users.Add(client);
                _context.SaveChanges();

                return Content("Added user");
            }
            catch (Exception ex)
            {
                return Content("Error:" + ex.Message);
            }
        }

        [HttpPost("loginUser")]
        public IActionResult CheckLogin([FromBody]UserViewModel model)
        {
            var user = _context.Users.FirstOrDefault(t => t.Login == model.Login && t.Password == model.Password );
            if (user != null)
            {
                return Ok(user.Id.ToString());
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/client/5
        [HttpPut("edit/{id}")]
        public ContentResult EditClient(int id, [FromBody]UserViewModel model)
        {
            var user = _context.Users.FirstOrDefault(t => t.Id == id);
            if (user != null)
            {
                user.Login = model.Login;
                user.Password = model.Password;
                user.Id = model.Id;
                _context.SaveChanges();
                return Content("User edited!");
            }
            return Content("User data = null!");
        }

        // DELETE api/client/delete
        [HttpDelete("delete/{id}")]
        public ContentResult DeleteClient(int id)
        {
            var client = _context.Users.FirstOrDefault(t => t.Id == id);
            if (client != null)
            {
                _context.Users.Remove(client);
                _context.SaveChanges();
                return Content("User deleted!");
            }
            return Content("User not found!");
        }


    }
}