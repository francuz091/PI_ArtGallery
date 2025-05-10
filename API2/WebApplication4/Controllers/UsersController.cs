using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication4.Models;

namespace ArtGallery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ArtGalleryContext _context;

        public UsersController(ArtGalleryContext context)
        {
            _context = context;
        }
        [HttpGet("/Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var users = await _context.Users.ToListAsync();

            return Ok(users);

        }
        public class AuthRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }


        [HttpPost("/Login")]
        public async Task<ActionResult> Auth([FromBody] AuthRequest authRequest)
        {
            var user = _context.Users.Include(u => u.RoleType).SingleOrDefault(u => u.Username == authRequest.Username);

            if (user != null)
            {
              
                if (VerifyPassword(authRequest.Password, user.Password))
                {
                    // Lozinka je ispravna, korisnik je prijavljen
                    string json = JsonConvert.SerializeObject(user, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    return Ok(json);
                }
                else
                {
                    // Lozinka nije ispravna
                    return Unauthorized("Neispravna lozinka.");
                }
            }
            else
            {
                // Korisnik nije pronađen
                return Unauthorized("Korisnik nije pronađen.");
            }
        }

        [HttpPost("/Register")]
        public async Task<ActionResult> Register([FromBody] User model)
        {

            var user = new User { FirstName = model.FirstName, LastName = model.LastName, Username = model.Username, Email = model.Email, Password = HashPassword(model.Email) };
            
            var result = await _context.AddAsync(user);

            if (result != null)
            {                
                return Ok("Korisnik uspješno kreiran");
            }
            else
            {
                return NotFound("Korisnik nije kreiran.");
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
        }
        public static string HashPassword(string password)
        {
            // BCrypt hash (work factor = 10)
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

    }
}
