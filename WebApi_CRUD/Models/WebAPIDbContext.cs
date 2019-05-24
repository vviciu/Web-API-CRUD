using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApi_CRUD.Models
{
    public class WebAPIDbContext: DbContext
    {
        public WebAPIDbContext()
            : base("DefaultConnection")
        {
        }

        public static WebAPIDbContext Create()
        {
            return new WebAPIDbContext();
        }

        public DbSet<Book> Books { get; set; }
    }
}