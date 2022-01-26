using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Mini.Common.Helpers;

public static class SecurityKeyHelper
{
    public static (SymmetricSecurityKey sc, SymmetricSecurityKey ec) SymmetricSecurityKey(
        string pipeSeparatedValue, 
        HashAlgorithmName hashAlgorithmName)
    {
        string? hashName = hashAlgorithmName.Name;

        if (hashName == null)
            throw new InvalidOperationException("Hash algorithm name is null.");

        var values = pipeSeparatedValue.Split('|', StringSplitOptions.None);

        if (values.Length < 4)
            throw new InvalidDataException("Invalid pipe separated value.");

        byte[] salt = Encoding.UTF8.GetBytes(values[0]);
        byte[] pwd = Encoding.UTF8.GetBytes(values[1]);

        _ = int.TryParse(values[2], out int scIt);
        _ = int.TryParse(values[3], out int ecIt);

        return (
            new(new PasswordDeriveBytes(pwd, salt, hashName, scIt).GetBytes(64)),
            new(new PasswordDeriveBytes(pwd, salt, hashName, ecIt).GetBytes(32)));
    }
}
