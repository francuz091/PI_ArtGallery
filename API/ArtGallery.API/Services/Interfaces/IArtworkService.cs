using ArtGallery.API.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace ArtGallery.API.Services.Interfaces
{
    public interface IArtworkService
    {
        Task<ArtworkDTO> CreateArtworkAsync(ArtworkDTO artworkDto);
        Task<ValidationResult> ValidateArtworkAsync(ArtworkDTO artworkDto);
    }
}
