namespace ArtGallery.API.Models.Domain
{
    public class OrderItem
    {
        public int IDOrderItem { get; set; }
        public int OrderID { get; set; }
        public int ArtWorkID { get; set; }
    }
}
