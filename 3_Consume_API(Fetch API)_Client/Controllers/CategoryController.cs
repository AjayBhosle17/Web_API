using _3_Consume_API_Fetch_API__Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Configuration;

namespace _3_Consume_API_Fetch_API__Client.Controllers
{
    public class CategoryController : Controller
    {
        HttpClient client;
        public CategoryController()
        {
            client = new HttpClient();
            string apiUrl = ConfigurationManager.AppSettings["ApiBaseUrl"]; // in Config file added already apiBaseUrl
            client.BaseAddress = new Uri(apiUrl);
        }

        // GET: Category
        [HttpGet]
        public ActionResult Index()
        {
            List<Category> categories = new List<Category>();

            try
            {
                // Api Hit
                HttpResponseMessage response = client.GetAsync("category").Result;

                if (response.IsSuccessStatusCode)
                {
                    // come in picture serialization and deserialization
                    string jsonResult = response.Content.ReadAsStringAsync().Result;

                    ViewBag.Response = jsonResult; // Store raw JSON response in ViewBag
                    // Deserialize JSON to List<Category>
                    categories = JsonConvert.DeserializeObject<List<Category>>(jsonResult);
                }
                else
                {
                    ViewBag.Error = "Failed to fetch categories from API.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

            return View(categories);
        }

        [HttpGet]
        public Category GetById(int? Id)
        {
            Category category = null;

            try 
            {
                HttpResponseMessage response = client.GetAsync($"category/{Id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = response.Content.ReadAsStringAsync().Result;
                    category = JsonConvert.DeserializeObject<Category>(jsonResult);
                }
                else
                {
                    ViewBag.Error = "Failed to fetch category details from API.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

            return category;
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            try
            {
                Category category = GetById(id);
                return View(category);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                string request = JsonConvert.SerializeObject(category);
                StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json"); // mediatype

                HttpResponseMessage response = client.PostAsync("Category", requestBody).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.Error = "Failed to create category.";
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                Category category = GetById(id);
                return View(category);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            try
            {
                string request = JsonConvert.SerializeObject(category);
                StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync($"Category/{category.Id}/", requestBody).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.Error = "Failed to update category.";
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

            return View();
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                Category category = GetById(id);
                return View(category);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync($"category/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.Error = "Failed to delete category.";
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

            return View();
        }
    }
}
