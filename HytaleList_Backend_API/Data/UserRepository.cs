using HytaleList_Backend_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HytaleList_Backend_API.Data
{
    public class UserRepository
    {
        private readonly MyDbContext _dbContext;

        public UserRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            try
            {
                var user = await _dbContext.users
                    .FirstOrDefaultAsync(u => u.Username == username);
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[UserRepository]: GetUserByUsername(username) - Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateUser(User user)
        {
            try
            {
                _dbContext.users.Add(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[UserRepository]: CreateUser(user) - Exception: {ex.Message}");
                return false;
            }
        }

        public async Task<User> GetUserById(int userId)
        {
            try
            {
                var user = await _dbContext.users
                    .FirstOrDefaultAsync(u => u.Id == userId);
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[UserRepository]: GetUserById(userId) - Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UserExists(string username)
        {
            try
            {
                var findUser = await _dbContext.users.AnyAsync(u => u.Username == username);
                return findUser;
            }
            catch
            {
                Debug.WriteLine($"[UserRepository]: UserExists(username) - Exception occurred while checking user existence.");
                return false;
            }
        }
    }
}
