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
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string username, string password)
        {
            try
            {
                // Pozivanje metode za autentifikaciju
                var result = await repository.Login(username, password);

                // Proveravanje odgovora
                if (result == "Neispravna lozinka." || result == "Korisnik nije pronađen.")
                {
                    ModelState.AddModelError("", result);
                    return View();
                }
                else if (result.StartsWith("Greška prilikom prijave:"))
                {
                    ModelState.AddModelError("", result);
                    return View();
                }

                // Ako je uspešno, redirektuj korisnika
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