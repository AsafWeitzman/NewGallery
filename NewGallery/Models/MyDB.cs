using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewGallery.Models
{
    public class MyDB : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Paint> Paints { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public System.Data.Entity.DbSet<NewGallery.Models.Comment> Comments { get; set; }
    }
}