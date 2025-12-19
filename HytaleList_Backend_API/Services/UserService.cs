using HytaleList_Backend_API.Data;
using HytaleList_Backend_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HytaleList_Backend_API.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string?> AuthenticateUser(UserDTO userDto)
        {
            var user = await _userRepository.GetUserByUsername(userDto.Username!);

            if (user == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(userDto.PasswordHash!, user.PasswordHash!))
            {
                return null;
            }

            string token = CreateJWT(user);
            Debug.WriteLine(token);

            return token;
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
                EmailVerified = false,
                ListedServers = 0
            };

            var result = await _userRepository.CreateUser(user);
            return result;
        }

        public async Task<User>? GetUserById(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return user;
        }

        public string CreateJWT(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username!),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
