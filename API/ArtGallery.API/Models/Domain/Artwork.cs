using System.ComponentModel.DataAnnotations;

namespace ArtGallery.API.Models.Domain
{
    public class Artwork
    {
        public int IDArtWork { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public int UserID { get; set; }
        public int ArtWorkTypeID { get; set; }

        public virtual User User { get; set; }
        public virtual ArtWorkType ArtWorkType { get; set; }
    }

}
