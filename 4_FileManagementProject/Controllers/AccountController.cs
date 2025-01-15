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
        FileDbContext db = new FileDbContext();

       
        public ActionResult Login()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == username && u.Password == password && u.IsActive);
            if (user != null)
            {
                Session["UserID"] = user.UserID;
                Session["UserName"] = user.UserName;
                Session["UserRole"] = user.Role;
                return RedirectToAction("Index", "File");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

    }
}