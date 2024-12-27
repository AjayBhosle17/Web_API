using _1_Web_API_Intro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _1_Web_API_Intro.Controllers
{
    public class CategoryController : ApiController
    {

        // regular c# method
        /*  public List<Category> GetAll()
          {
              List<Category> categories = new List<Category>()
            {
                new Category(){Id=1,Name="Mens Wear",Rating=5},
                new Category(){ Id=2,Name="Kids Wear",Rating=4}
            };

              return categories;
          }*/

        // use httpResponseMessage

        
       /* public HttpResponseMessage GetAll()
        {
            List<Category> categories = new List<Category>() {
              new Category(){Id=1,Name="Mens Wear",Rating=5},
              new Category(){ Id=2,Name="Kids Wear",Rating=4}
            };

            return Request.CreateResponse(HttpStatusCode.OK, categories);
        }
*/

        // use IHttpActionResult

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Category> categories = new List<Category>() {
              new Category(){Id=1,Name="Mens Wear",Rating=5},
              new Category(){ Id=2,Name="Kids Wear",Rating=4}
            };

            return Ok(categories);
        }



    }
}
