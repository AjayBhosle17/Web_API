using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using _2_Method_Para_With_Crud_IN_API.Models.Entities;

namespace _2_Method_Para_With_Crud_IN_API.Models
{
    public class ProductDBContext:DbContext
    {
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Users> Users { get; set; }

    }
}