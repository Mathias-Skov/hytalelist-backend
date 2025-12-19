using HytaleList_Backend_API.Data;
using HytaleList_Backend_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HytaleList_Backend_API.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AuthenticateUser(UserDTO userDto)
        {
            var user = await _userRepository.GetUserByUsername(userDto.Username!);

            if (user == null)
            {
                return false;
            }

            if(user.Username != userDto.Username)
            {
                return false;
            }

            if (!BCrypt.Net.BCrypt.Verify(userDto.PasswordHash!, user.PasswordHash!))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CreateUser(UserDTO userDto)
        {
            var existingUser = await _userRepository.UserExists(userDto.Username!);

            // Check for duplicates
            if (existingUser)
            {
                return false;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.PasswordHash!);

            var user = new User
            {
                Username = userDto.Username,
                PasswordHash = passwordHash,
                Email = userDto.Email,
                DateCreated = DateTime.UtcNow,
                ListedServers = 0
            };

            var result = await _userRepository.CreateUser(user);
            return result;
        }

        public string CreateJWT(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
