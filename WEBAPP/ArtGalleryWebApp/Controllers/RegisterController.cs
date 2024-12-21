using ArtGalleryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtGalleryWebApp.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("PasswordMismatch", "Passwords do not match!");
                return View("Index", model);
            }
            return RedirectToAction("Success");
        }
    }

}
