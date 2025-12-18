namespace HytaleList_Backend_API.Services
{
    public interface IPasswordService
    {
        (string Hash, string Salt) HashPassword(string password);
        bool VerifyPassword(string password, string storedHash, string storedSalt);
    }
}
