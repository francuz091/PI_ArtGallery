using ArtGalleryAPI.Models.DTO;
using ArtGalleryWebApp.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArtGalleryWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly Repository repository;

        public LoginController()
        {
            repository = new Repository();
        }
        [HttpGet]
        public ActionResult Index()
        {
            var model = new AuthRequestDTO();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(AuthRequestDTO authRequest)
        {
            try
            {
                // Pozivanje metode za autentifikaciju
                var result = await repository.Login(authRequest);

                
                if (result.ErrorMessage !=null)
                {
                    ModelState.AddModelError("", result.ErrorMessage);
                    return View(authRequest);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Obrada neočekivane greške
                ModelState.AddModelError("", $"Neočekivana greška: {ex.Message}");
                return View();
            }
        }

    }
}