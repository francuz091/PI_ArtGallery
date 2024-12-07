using System.ComponentModel.DataAnnotations;

namespace ArtGallery.API.Models.DTO
{

    public class ArtworkDTO
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        public IFormFile Picture { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
