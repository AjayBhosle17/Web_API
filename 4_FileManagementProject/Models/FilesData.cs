using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4_FileManagementProject.Models
{
    public class FilesData
    {
        [Key]
        public int FileID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public int UploadedBy { get; set; }
        public DateTime UploadDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
        public string EntryIP { get; set; }
        public string UpdateIP { get; set; }

        public User UploadedUser { get; set; }
    }
}