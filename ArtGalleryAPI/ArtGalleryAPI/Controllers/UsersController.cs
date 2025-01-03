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
            if (string.IsNullOrWhiteSpace(authRequest.Password)) 
                return BadRequest(new { Message = "Password is required." });
            if (string.IsNullOrWhiteSpace(authRequest.Username)) 
                return BadRequest(new { Message = "Username is required." });

            var userDTO = await _userService.GetUserByUsernameAsync(authRequest.Username);

            if (userDTO != null)
            {
                if (SecurityHelper.VerifyPassword(authRequest.Password, userDTO.Password))
                {
                    string json = JsonConvert.SerializeObject(userDTO);

                    return Ok(json);
                }
                else
                { return Unauthorized("Neispravna lozinka."); }
                
            }
            else
            { return Unauthorized("Korisnik nije pronađen."); }
        }

        [HttpPost("/Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO model)
        {
            var result = await _userService.PostUserAsync(model);

            if (result.Success)
            {
                
                return Ok(new { Message = "Korisnik uspješno kreiran", User = result.Data });
            }
            
            return BadRequest(new { Message = result.ErrorMessage });
        }  

    }
}
