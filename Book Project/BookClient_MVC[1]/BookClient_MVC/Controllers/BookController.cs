using BookClient_MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BookClient_MVC.Controllers
{
    public class BookController : Controller
    {
        // GET: Book

        private readonly ApiTaskEntities _context;



        string apiUrl = string.Empty;

        public BookController()
        {
            apiUrl = ConfigurationManager.AppSettings["apiUrl"]?.ToString();
            _context = new ApiTaskEntities();


        }

        public ActionResult Index()
        {
            List<Book> books = new List<Book>();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);

            HttpResponseMessage response =
                client.GetAsync("book").Result;

            if (response.IsSuccessStatusCode)
            {
                string responseContent =
                    response.Content.ReadAsStringAsync().Result; // json content
                // deserialize
                books = JsonConvert.
                    DeserializeObject<List<Book>>(responseContent);
            }
            else
            {
                ViewBag.Message = "Categories not available";
            }


            return View(books);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Create(Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(apiUrl);

        //        string content = JsonConvert.SerializeObject(book);

        //        StringContent json = new StringContent(content,
        //            Encoding.UTF8, "application/json");

        //        HttpResponseMessage response =
        //            client.GetAsync("book").Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    return View(); 
        //}

        [HttpPost]
        public ActionResult Create(Book book, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Handle the uploaded image file
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    // Generate a unique file name and save the file
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;

                    string folderPath = Server.MapPath("~/Images/");
                    Directory.CreateDirectory(folderPath); // Ensure directory exists
                    string filePath = Path.Combine(folderPath, fileName);
                    ImageFile.SaveAs(filePath);

                    // Save the relative path to the model
                    book.BookCoverPhoto = "~/Images/" + fileName;
                }

                // Exclude the ImageFile property from serialization
                var bookToSend = new
                {
                    book.Title,
                    book.Author,
                    book.PublishedDate,
                    book.ISBN,
                    book.BookCoverPhoto
                };

                // Serialize the book data
                string content = JsonConvert.SerializeObject(bookToSend);

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    StringContent json = new StringContent(content, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("book", json).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(book);
        }

        [NonAction]
        public Book GetById(int? id)
        {
            if (id == null) return null;

            Book book = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                HttpResponseMessage response = client.GetAsync($"book/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = response.Content.ReadAsStringAsync().Result;
                    book = JsonConvert.DeserializeObject<Book>(jsonContent);
                }
            }

            return book;
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Book book = GetById(id);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Handle the uploaded image file
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;

                    string folderPath = Server.MapPath("~/Images/");
                    Directory.CreateDirectory(folderPath); // Ensure directory exists
                    string filePath = Path.Combine(folderPath, fileName);
                    ImageFile.SaveAs(filePath);

                    // Save the relative path to the model
                    book.BookCoverPhoto = "~/Images/" + fileName;
                }

                // Serialize only the relevant book properties
                var bookData = new
                {
                    book.ID,
                    book.Title,
                    book.Author,
                    book.PublishedDate,
                    book.ISBN,
                    book.BookCoverPhoto
                };

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiUrl);

                string content = JsonConvert.SerializeObject(bookData);
                StringContent jsonRequestBody = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync($"book/{book.ID}", jsonRequestBody).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(GetById(book.ID));
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Book book = GetById(id);
            return View(book);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_Confirmed(int Id)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(apiUrl);

            //HttpResponseMessage response =
            //    client.DeleteAsync($"category/{Id}").Result;
            Book book = GetById(Id);
            string content = JsonConvert.SerializeObject(book);
            StringContent json = new StringContent(content,
                Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                client.DeleteAsync($"book/{Id}").Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            //Category category = GetById(Id);
            return View(book);
        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "ID is required.");
            }

            // Fetch the book with the given ID
            var book = _context.Books.FirstOrDefault(b => b.ID == id);

            // If no book is found, return a 404 error
            if (book == null)
            {
                return HttpNotFound("Book not found.");
            }

            // Ensure that the BookCoverPhoto has a valid URL (relative path)
            if (!string.IsNullOrEmpty(book.BookCoverPhoto))
            {
                // Resolve the relative path if necessary
                book.BookCoverPhoto = Url.Content(book.BookCoverPhoto);
            }

            return View(book);
        }


         

    }
}