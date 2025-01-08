namespace ArtGalleryAPI.Models.DTO
{
    public class UserDTO
    {
        public int Iduser { get; set; }

        public string Username { get; set; } 

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public byte[] Picture { get; set; }
        public int? RoleTypeId { get; set; }

        public string RoleType { get; set; }
    }
}
