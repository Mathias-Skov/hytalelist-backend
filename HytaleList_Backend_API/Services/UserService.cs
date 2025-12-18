using HytaleList_Backend_API.Data;
using HytaleList_Backend_API.Models;

namespace HytaleList_Backend_API.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public UserService(UserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                return false;
            }
            var isPasswordValid = _passwordService.VerifyPassword(password, user.PasswordHash!, user.Salt!);
            return isPasswordValid;
        }

        public async Task<bool> CreateUser(string username, string password, string email)
        {
            var existingUser = await _userRepository.UserExists(username);

            // Check for duplicates
            if (existingUser)
            {
                return false;
            }

            var (hash, salt) = _passwordService.HashPassword(password);

            var user = new User
            {
                Username = username,
                PasswordHash = hash,
                Salt = salt,
                Email = email,
                DateCreated = DateTime.UtcNow,
                ListedServers = 0
            };

            var result = await _userRepository.CreateUser(user);
            return result;
        }
    }
}
