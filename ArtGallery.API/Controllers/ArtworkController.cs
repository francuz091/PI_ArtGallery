using ArtGallery.API.Models.DTO;
using ArtGallery.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ArtGallery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtworkController : ControllerBase
    {
        private readonly IArtworkService _artworkService;
        private readonly ILogger<ArtworkController> _logger;

        public ArtworkController(IArtworkService artworkService, ILogger<ArtworkController> logger)
        {
            _artworkService = artworkService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateArtwork([FromForm] ArtworkDTO artworkDto)
        {
            try
            {
                _logger.LogInformation("Creating new artwork: {Title}", artworkDto.Title);
                var result = await _artworkService.CreateArtworkAsync(artworkDto);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
