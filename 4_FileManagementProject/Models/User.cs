using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4_FileManagementProject.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; } // Use hashing for security

        [Required]
        [StringLength(20)]
        public string Role { get; set; } // Admin or User

        public bool IsActive { get; set; }

        public virtual ICollection<FilesData> Files { get; set; }
    }

}