using ArtGalleryAPI.Models;
using ArtGalleryAPI.Models.DTO;
using ArtGalleryAPI.Security;
using ArtGalleryAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ArtGallery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        public class UsersController : ControllerBase
        {

        private readonly UserService _userService;
        private readonly ArtGalleryContext _context;

            public UsersController(ArtGalleryContext context, UserService userService)
            {
                _context = context;
                _userService = userService;
            }


            [HttpGet("/Users")]
            public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
            {
                if (_context.Users == null)
                {
                    return NotFound();
                }
                var users = await _userService.GetUsersAsync();

                return Ok(users);

            }
        


        [HttpPost("/Login")]
        public async Task<ActionResult> Auth([FromBody] AuthRequestDTO authRequest)
        {

            var result = await _userService.LoginUserAsync(authRequest);

            if (result.Success)
            {
                return Ok(new { Message = "Korisnik uspješno logiran", User = result.Data });
            }

            return BadRequest(new { Message = result.ErrorMessage });
        }  
        

        [HttpPost("/Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO model)
        {
            var result = await _userService.RegisterUserAsync(model);

            if (result.Success)
            {                
                return Ok(new { Message = "Korisnik uspješno kreiran", User = result.Data });
            }
            
            return BadRequest(new { Message = result.ErrorMessage });
        }  

    }
}
