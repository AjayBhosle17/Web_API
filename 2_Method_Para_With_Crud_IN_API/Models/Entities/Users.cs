using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2_Method_Para_With_Crud_IN_API.Models.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}