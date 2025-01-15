using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_Data.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.temp = "My name is Ajay Bhosle";
            ViewData["xyz"] = "Hello";
            return View();
        }
    }
}