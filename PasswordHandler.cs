using PasswordHasher;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHandler
{
    private const int SaltLength = 8;

    public static (string hash, string salt) HashPassword(string password)
    { 
        string salt = GenerateSalt();

        string saltedPassword = password + salt;

        byte[] hashBytes = BlockCipher.EncryptData(saltedPassword);

        return (Convert.ToBase64String(hashBytes), salt);
    }

    public static bool VerifyPassword(string password, string hash, string salt)
    {
        string saltedPassword = password + salt;
        byte[] testHashBytes = BlockCipher.EncryptData(saltedPassword);

        return hash == Convert.ToBase64String(testHashBytes);
    }

    private static string GenerateSalt()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] saltBytes = new byte[SaltLength];
            rng.GetBytes(saltBytes);

            string base64Salt = Convert.ToBase64String(saltBytes);
            return base64Salt.Substring(0, SaltLength);
        }
    }
}