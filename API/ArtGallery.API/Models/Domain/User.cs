namespace ArtGallery.API.Models.Domain
{
    public class User
    {
        public int IDUser { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Picture { get; set; }
        public int RoleTypeID { get; set; }
    }
}
