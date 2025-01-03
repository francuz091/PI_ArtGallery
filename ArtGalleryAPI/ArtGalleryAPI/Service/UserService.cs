using ArtGalleryAPI.Models;
using ArtGalleryAPI.Models.DTO;
using ArtGalleryAPI.Response;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

            if (user == null) return null;           

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

        public async Task<BaseResponse<UserDTO>> PostUserAsync(RegisterDTO userDTO)
        {       

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == userDTO.Username);

            if (existingUser != null)            
                return BaseResponse<UserDTO>.FailureResult("Username se vec koristi");
            

            RoleType roleType = await _context.RoleTypes.SingleAsync(r => r.Type == "USER");

            var user = new User
            {               
                Username = userDTO.Username,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = userDTO.Password,
                Picture = null,
                RoleTypeId = roleType.IdroleType,               
            };

            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();            

            return BaseResponse<UserDTO>.SuccessResult(new UserDTO
            {
                Iduser = user.Iduser,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Picture = user.Picture,
                RoleTypeId = roleType.IdroleType,
                RoleType = roleType.Type
            });
        }
    }
}
