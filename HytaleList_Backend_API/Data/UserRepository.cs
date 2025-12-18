namespace HytaleList_Backend_API.Data
{
    public class UserRepository
    {
        private readonly MyDbContext _dbContext;

        public UserRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateUser(string username, string email, string passwordHash)
        {
            try
            {
                var user = new Models.User
                {
                    Username = username,
                    Email = email,
                    PasswordHash = passwordHash
                };
                _dbContext.users.Add(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[UserRepository]: CreateUser() - Exception: {ex.Message}");
                return false;
            }
        }
    }
}
