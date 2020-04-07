using System;
using System.Security.Cryptography;

public class PasswordHasher
{
    public static string ComputeHash(string password)
    {
        var salt = "3v3nt.St@ck";
        var bytesPassword = Convert.FromBase64String(password + salt);

        var hashAlgorithm = new SHA512Managed();
        var hashedPassword = hashAlgorithm.ComputeHash(bytesPassword);

        return Convert.ToBase64String(hashedPassword);
    }
}
