using ArtGallery.API.Data.Repositories;
using ArtGallery.API.Models.Domain;
using ArtGallery.API.Models.DTO;
using ArtGallery.API.Services.Interfaces;

namespace ArtGallery.API.Services.Implementations
{
    public class ArtworkService : IArtworkService
    {
        private readonly IArtworkRepository _repository;
        private readonly ILogger<ArtworkService> _logger;

        public ArtworkService(IArtworkRepository repository, ILogger<ArtworkService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ArtworkDTO> CreateArtworkAsync(ArtworkDTO artworkDto)
        {
            try
            {
                // Validacija
                var validationResult = await ValidateArtworkAsync(artworkDto);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Artwork validation failed: {Errors}",
                        string.Join(", ", validationResult.Errors));
                    throw new ValidationException(validationResult.Errors);
                }

                // Konverzija DTO u domain model
                var artwork = new Artwork
                {
                    Title = artworkDto.Title,
                    Description = artworkDto.Description,
                    Price = artworkDto.Price,
                    PublicationDate = DateTime.UtcNow,
                    // Pretvaranje IFormFile u byte array
                    Picture = await ConvertToByteArrayAsync(artworkDto.Picture)
                };

                // Spremanje u bazu
                var createdArtwork = await _repository.CreateAsync(artwork);

                _logger.LogInformation("Artwork successfully created with ID: {Id}",
                    createdArtwork.IDArtWork);

                // Konverzija natrag u DTO
                return new ArtworkDTO
                {
                    Title = createdArtwork.Title,
                    Description = createdArtwork.Description,
                    Price = createdArtwork.Price
                };
            }
            catch (Exception ex) when (ex is not ValidationException)
            {
                _logger.LogError(ex, "Error occurred while creating artwork");
                throw;
            }
        }

        public async Task<ValidationResult> ValidateArtworkAsync(ArtworkDTO artworkDto)
        {
            var validationResult = new ValidationResult();

            try
            {
                // Validacija naslova
                if (string.IsNullOrWhiteSpace(artworkDto.Title))
                {
                    validationResult.Errors.Add("Title is required");
                }
                else if (artworkDto.Title.Length > 50)
                {
                    validationResult.Errors.Add("Title cannot exceed 50 characters");
                }

                // Validacija opisa
                if (artworkDto.Description?.Length > 150)
                {
                    validationResult.Errors.Add("Description cannot exceed 150 characters");
                }

                // Validacija cijene
                if (artworkDto.Price <= 0)
                {
                    validationResult.Errors.Add("Price must be greater than 0");
                }

                // Validacija slike
                if (artworkDto.Picture == null)
                {
                    validationResult.Errors.Add("Picture is required");
                }
                else
                {
                    if (artworkDto.Picture.Length > 10 * 1024 * 1024) // 10MB
                    {
                        validationResult.Errors.Add("Picture size cannot exceed 10MB");
                    }

                    var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                    if (!allowedTypes.Contains(artworkDto.Picture.ContentType.ToLower()))
                    {
                        validationResult.Errors.Add("Invalid file type. Only JPG, PNG and GIF are allowed");
                    }
                }

                _logger.LogInformation("Artwork validation completed with {ErrorCount} errors",
                    validationResult.Errors.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during artwork validation");
                validationResult.Errors.Add("An error occurred during validation");
            }

            return validationResult;
        }

        private async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        Task<System.ComponentModel.DataAnnotations.ValidationResult> IArtworkService.ValidateArtworkAsync(ArtworkDTO artworkDto)
        {
            throw new NotImplementedException();
        }
    }

    // Pomocna klasa za validaciju
    public class ValidationResult
    {
        public List<string> Errors { get; set; } = new List<string>();
        public bool IsValid => !Errors.Any();
    }

    // Custom exception za validaciju
    public class ValidationException : Exception
    {
        public ValidationException(IEnumerable<string> errors)
            : base(string.Join(", ", errors))
        {
        }
    }
}
