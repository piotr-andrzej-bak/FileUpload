using FileUpload.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FileUpload.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment _webHostEnvironment;
        //private var uniqeFileName;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SingleFile(IFormFile file)
        {
            if (file != null)
            {
                var dir = _webHostEnvironment.ContentRootPath + "uploads/";
                Guid uniqeFileName = Guid.NewGuid();
                var fileNameUniqe = uniqeFileName.ToString();
                var fileExt = file.FileName.Substring(file.FileName.IndexOf("."));
                if (fileExt == ".png")
                {
                    using (var fileStream = new FileStream(Path.Combine(dir, fileNameUniqe + fileExt), FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fileStream);
                    }
                    TempData["Error"] = "uploaded";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "format error";
                    return RedirectToAction("Index");
                }
            }
            TempData["Error"] = "lenght error";
            return RedirectToAction("Index");


        }
    }
}