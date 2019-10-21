using ClassLibrary1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HideAndDeliver.Entities
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options)
        : base(options)
        { }
        public DbSet<Kladman> Kladmans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
