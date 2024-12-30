using _3_Consume_API_Fetch_API__Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace _3_Consume_API_Fetch_API__Client.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        [HttpGet]
        public ActionResult Index()
        {

            // list of category fetch using API

            List<Category> categories= new List<Category>();

            //Api Hit

            HttpClient client = new HttpClient();  // api call/request


            //api url => https://localhost:44371/

            client.BaseAddress = new Uri("https://localhost:44371/api/");

            HttpResponseMessage response=client.GetAsync("category").Result;


            if (response.IsSuccessStatusCode)
            {
                // come in picture serialization and de serialization

               // ViewBag.Response=response.Content.ReadAsStringAsync().Result;

                // json to list<string> use deserialztion and install package newtonSoft.json


                string jsonresult = response.Content.ReadAsStringAsync().Result;

                ViewBag.Response=jsonresult;
                //de serialization

                categories = JsonConvert.DeserializeObject<List<Category>>(jsonresult);



            }

            client.Dispose();  // close and dispose connection Object

            return View(categories);
        }

        [HttpGet]
        public Category GetById(int? Id)
        {
         
            Category category = null;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44371/api/");

            HttpResponseMessage response = client.GetAsync($"category/{Id.Value}").Result;

            if (response.IsSuccessStatusCode)
            {
                string jsonresult = response.Content.ReadAsStringAsync().Result;
                category= JsonConvert.DeserializeObject<Category>(jsonresult);

            }

            client.Dispose();
            return category;
        }

        [HttpGet]


    }
}