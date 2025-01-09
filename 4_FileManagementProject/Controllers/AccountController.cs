using _4_FileManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4_FileManagementProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        FileDbContext db = new FileDbContext();
        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user != null)
            {
                Session["UserID"] = user.UserID;
                Session["UserRole"] = user.Role; // e.g., "Admin" or "User"
                return RedirectToAction("Index", "File");
            }

            TempData["Error"] = "Invalid username or password.";
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}