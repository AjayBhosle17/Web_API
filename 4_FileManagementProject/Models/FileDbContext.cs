using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _4_FileManagementProject.Models
{
    public class FileDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<FilesData> Files { get; set; }
    }
}