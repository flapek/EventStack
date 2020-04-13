using System;
using System.Security.Cryptography;

public class PasswordHasher
{
    private const string salt = "3v3nt.St@ck";

    public static string ComputeHash(string password)
    {
        var bytesPassword = Convert.FromBase64String(password + salt);

        var hashAlgorithm = new SHA512Managed();
        var hashedPassword = hashAlgorithm.ComputeHash(bytesPassword);

        return Convert.ToBase64String(hashedPassword);
    }
}
