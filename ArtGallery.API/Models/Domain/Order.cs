namespace ArtGallery.API.Models.Domain
{
    public class Order
    {
        public int IDOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserID { get; set; }
        public int PaymentTypeID { get; set; }
    }
}
