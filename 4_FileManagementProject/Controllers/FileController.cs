using _4_FileManagementProject.Filters;
using _4_FileManagementProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace _4_FileManagementProject.Controllers
{
    [RoleAuthorize(Roles = new[] { "Admin", "User" })]
    public class FileController : Controller
    {
       
        
        
            FileDbContext _fileContext = new FileDbContext();

            // GET: File
            public ActionResult Index()
            {
                var userRole = Session["UserRole"].ToString();
                var userId = Convert.ToInt32(Session["UserID"]);

                var files = userRole == "Admin"
                    ? _fileContext.Files.ToList()
                    : _fileContext.Files.Where(f => f.UploadedBy == userId).ToList();

                return View(files);
            }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserRole"] == null)
            {
                // Temporarily assign a default role (e.g., Admin or User) for testing
                Session["UserRole"] = "Admin";
                Session["UserID"] = 1; // Set a test UserID
            }
            base.OnActionExecuting(filterContext);
        }

        // GET: File/Upload
        public ActionResult Upload()
            {
                return View();
            }

            // POST: File/Upload
            [HttpPost]
            public ActionResult Upload(HttpPostedFileBase file)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    file.SaveAs(filePath);

                    var newFile = new FilesData
                    {
                        FileName = fileName,
                        FilePath = filePath,
                        FileType = Path.GetExtension(fileName),
                        UploadedBy = Convert.ToInt32(Session["UserID"]),
                        UploadDate = DateTime.Now,
                        EntryIP = Request.UserHostAddress
                    };

                    _fileContext.Files.Add(newFile);
                    _fileContext.SaveChanges();

                    TempData["Success"] = "File uploaded successfully.";
                    return RedirectToAction("Index");
                }

                TempData["Error"] = "File upload failed.";
                return View();
            }

            // GET: File/Edit
            [RoleAuthorize(Roles = new[] { "Admin" })]
            public ActionResult Edit(int id)
            {
                var file = _fileContext.Files.Find(id);
                return View(file);
            }

            // POST: File/Edit
            [HttpPost]
            [RoleAuthorize(Roles = new[] { "Admin" })]
            public ActionResult Edit(FilesData file)
            {
                var existingFile = _fileContext.Files.Find(file.FileID);
                if (existingFile != null)
                {
                    existingFile.FileName = file.FileName;
                    existingFile.IsActive = file.IsActive;
                    existingFile.UpdateDate = DateTime.Now;
                    existingFile.UpdateIP = Request.UserHostAddress;

                    _fileContext.SaveChanges();
                    TempData["Success"] = "File updated successfully.";
                    return RedirectToAction("Index");
                }

                TempData["Error"] = "File not found.";
                return View(file);
            }

            // GET: File/Delete
            [RoleAuthorize(Roles = new[] { "Admin" })]
            public ActionResult Delete(int id)
            {
                var file = _fileContext.Files.Find(id);
                if (file != null)
                {
                    _fileContext.Files.Remove(file);
                    _fileContext.SaveChanges();
                    TempData["Success"] = "File deleted successfully.";
                }
                else
                {
                    TempData["Error"] = "File not found.";
                }
                return RedirectToAction("Index");
            }

            // GET: File/Download
            public ActionResult Download(int id)
            {
                var file = _fileContext.Files.Find(id);
                if (file != null)
                {
                    var fileBytes = System.IO.File.ReadAllBytes(file.FilePath);
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.FileName);
                }

                TempData["Error"] = "File not found.";
                return RedirectToAction("Index");
            }
        
    }
}