using KaraHan.Application.Abstractions;
using System.Security.Cryptography;

namespace KaraHan.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    public (string Hash, string Salt) Hash(string password)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(16);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100_000, HashAlgorithmName.SHA256);
        var hashBytes = pbkdf2.GetBytes(32);

        return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
    }

    public bool Verify(string password, string hash, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100_000, HashAlgorithmName.SHA256);
        var computedHash = pbkdf2.GetBytes(32);
        var expectedHash = Convert.FromBase64String(hash);

        return CryptographicOperations.FixedTimeEquals(computedHash, expectedHash);
    }
}
