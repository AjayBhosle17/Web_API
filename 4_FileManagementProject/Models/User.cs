using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4_FileManagementProject.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }    // "Admin" or "User"
        public bool IsActive { get; set; }

        public ICollection<FilesData> Files { get; set; }
    }
}