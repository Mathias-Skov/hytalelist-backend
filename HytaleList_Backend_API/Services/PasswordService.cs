using System.Security.Cryptography;

namespace HytaleList_Backend_API.Services
{
    public class PasswordService : IPasswordService
    {
        private const int SaltSize = 16;       // 128-bit
        private const int KeySize = 32;       // 256-bit
        private const int Iterations = 100_000;

        public (string Hash, string Salt) HashPassword(string password)
        {
            Span<byte> salt = stackalloc byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt.ToArray(),
                Iterations,
                HashAlgorithmName.SHA256,
                KeySize);

            return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
        }

        public bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var expectedHash = Convert.FromBase64String(storedHash);

            byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                saltBytes,
                Iterations,
                HashAlgorithmName.SHA256,
                expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
    }
}
