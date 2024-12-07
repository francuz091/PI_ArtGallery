namespace ArtGallery.API.Models.Domain
{
    public class ArtWorkType
    {
        public int IDArtWorkType { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Artwork> Artworks { get; set; }
    }
}
