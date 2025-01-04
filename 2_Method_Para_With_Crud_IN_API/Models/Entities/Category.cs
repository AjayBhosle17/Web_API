using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _2_Method_Para_With_Crud_IN_API.Models.Entities
{
    [Table("Category")]
    public class Category
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="varchar")]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [Range(1,5)]
        public int Rating { get; set; }


        //use for Managing API using Post all opaertion in CategoryV2Controller 

       /* public bool IsCreate { get; set; }

        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }*/
    }
}