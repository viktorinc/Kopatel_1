﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string Login { get; set; }

        public string Password { get; set; }



    }
}
