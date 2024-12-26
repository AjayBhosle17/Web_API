using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _1_Web_API_Intro.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        [Route("")]
        public IEnumerable<string> GetNames()
        {
            return new List<string>() { "Parmeshwar", "Avinash" };
        }

        [Route("{id:int}")]
        public string GetName(int id)
        {
            return $"{id} : ajay";
        }

        [Route("getName2/{sid:int}")]
        public string getName2(int sid) {

            return $"{sid}: Vijay";
        }
    }
}
