using System.Security.Cryptography;
using System.Text;

namespace AirportTicketBookingSystem.Utilities;

public static class PasswordHasher
{
    private static readonly int _keySize = 64;  
    private static readonly int _iterations = 350000;  
    private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;  

    public static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_keySize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            _iterations,
            _hashAlgorithm,
            _keySize);

        string saltBase64 = Convert.ToBase64String(salt);
        string hashBase64 = Convert.ToBase64String(hash);

        // Return the salt and hash, separated by a colon
        return $"{saltBase64}:{hashBase64}";
    }

    public static bool VerifyPassword(string password, string hash)
    {
        var parts = hash.Split(':');
        if (parts.Length != 2) return false;

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] storedHash = Convert.FromBase64String(parts[1]);

        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            _iterations,
            _hashAlgorithm,
            _keySize);

        return CryptographicOperations.FixedTimeEquals(hashToCompare, storedHash);
    }
}
