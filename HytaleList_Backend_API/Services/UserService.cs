using HytaleList_Backend_API.Data;

namespace HytaleList_Backend_API.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> CreateUser(string username, string passwordHash, string email)
        {
            var result = await _userRepository.CreateUser(username, passwordHash, email);
            return result;
        }
    }
}
