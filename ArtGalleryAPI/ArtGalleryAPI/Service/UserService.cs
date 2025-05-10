using ArtGalleryAPI.Models;
using ArtGalleryAPI.Models.DTO;
using ArtGalleryAPI.Response;
using ArtGalleryAPI.Security;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public async Task<BaseResponse<UserDTO>> LoginUserAsync(AuthRequestDTO authRequest)
        {
            if (string.IsNullOrWhiteSpace(authRequest.Password))
                return BaseResponse<UserDTO>.FailureResult("Password is required.");
            if (string.IsNullOrWhiteSpace(authRequest.Username))
                return BaseResponse<UserDTO>.FailureResult("Username is required.");

            var user = await _context.Users
                .Include(u => u.RoleType)
                .SingleOrDefaultAsync(u => u.Username == authRequest.Username);

            if (user == null) 
                return BaseResponse<UserDTO>.FailureResult("Username not found");

            if (SecurityHelper.VerifyPassword(authRequest.Password, user.Password))
            {
                return BaseResponse<UserDTO>.SuccessResult(new UserDTO
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
                }); 
            }
            else
                return BaseResponse<UserDTO>.FailureResult("Neispravna lozinka");


        }

        public async Task<BaseResponse<UserDTO>> RegisterUserAsync(RegisterDTO userDTO)
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
                Password = SecurityHelper.HashPassword(userDTO.Password),
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
