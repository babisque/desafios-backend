using System.Security.Cryptography;

namespace Cryptography.Services;

public static class CryptographyServices
{
    public static byte[] GeneratePbkdf2Hash(string password)
    {
        var salt = GenerateSalt();
        var iterations = 10000;
        var hashByteSize = 32;

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(hashByteSize);
        return hash;
    }

    private static byte[] GenerateSalt(int size = 16)
    {
        var salt = new byte[size];
        RandomNumberGenerator.Fill(salt);

        return salt;
    }
}