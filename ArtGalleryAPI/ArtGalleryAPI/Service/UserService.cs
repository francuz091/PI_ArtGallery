using ArtGalleryAPI.Models;
using ArtGalleryAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Service
{
    public class UserService
    {
        private readonly ArtGalleryContext _context;

        public UserService(ArtGalleryContext context)
        {
            _context = context;
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var users = await _context.Users.Include(u => u.RoleType).ToListAsync();

            // Mapirajte entitete u DTO
            var usersDTO = users.Select(user => new UserDTO
            {
                Iduser = user.Iduser,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Picture = user.Picture,
                RoleTypeId = user.RoleType.IdroleType,
                RoleType = user.RoleType.Type
            }).ToList();

            return usersDTO;
        }

        public async Task<UserDTO?> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.RoleType)
                .SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return null;
            }

            // Mapiramo entitet u DTO
            return new UserDTO
            {
                Iduser = user.Iduser,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Picture = user.Picture,
                RoleTypeId = user.RoleType.IdroleType,
                RoleType = user.RoleType.Type
            };
        }
    }
}
