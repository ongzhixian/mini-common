using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Mini.Common.Models;
using Mini.Common.Settings;

namespace Mini.Common.Services;

public interface IPkedService
{
    Task<T?> DecryptAsync<T>(EncryptedMessage message, string encryptionKeyName);
    Task<EncryptedMessage> EncryptAsync<T>(T data, string encryptionKeyName);
}

public class PkedService : IPkedService
{
    private readonly IOptionsSnapshot<RsaKeySetting> rsaKeySettingOptions;

    public PkedService(IOptionsSnapshot<RsaKeySetting> rsaKeySettingOptions)
    {
        this.rsaKeySettingOptions = rsaKeySettingOptions;
    }

    public async Task<EncryptedMessage> EncryptAsync<T>(T data, string encryptionKeyName)
    {
        RsaKeySetting recepRsaKeySetting = rsaKeySettingOptions.Get(encryptionKeyName);

        using RSA rsaKey = RSA.Create();
        using Aes aes = Aes.Create();
        using MemoryStream cipherBytes = new();
        using CryptoStream cryptoStream = new(cipherBytes, aes.CreateEncryptor(), CryptoStreamMode.Write);
        rsaKey.FromXmlString(await recepRsaKeySetting.GetRsaSecurityKeyXmlAsync(false));

        RSAOAEPKeyExchangeFormatter keyFormatter = new(rsaKey);

        byte[] encryptedSessionKey = keyFormatter.CreateKeyExchange(aes.Key, typeof(Aes));

        byte[] iv = aes.IV;

        // Encrypt the message

        string jsonData = JsonSerializer.Serialize(data);

        byte[] jsonDataBytes = Encoding.UTF8.GetBytes(jsonData);

        cryptoStream.Write(jsonDataBytes, 0, jsonDataBytes.Length);

        cryptoStream.FlushFinalBlock();

        cryptoStream.Close();

        byte[] encryptedMessage = cipherBytes.ToArray();

        return new EncryptedMessage(iv, encryptedSessionKey, encryptedMessage);
    }

    public async Task<EncryptedMessage> EncryptAsync<T>(T data, string encryptionKeyName, string enclosedPublicKeyName)
    {
        RsaKeySetting recepRsaKeySetting = rsaKeySettingOptions.Get(encryptionKeyName);
        RsaKeySetting myPublicKeySetting = rsaKeySettingOptions.Get(enclosedPublicKeyName);

        using RSA rsaKey = RSA.Create();
        using Aes aes = Aes.Create();
        using MemoryStream cipherBytes = new();
        using CryptoStream cryptoStream = new(cipherBytes, aes.CreateEncryptor(), CryptoStreamMode.Write);
        rsaKey.FromXmlString(await recepRsaKeySetting.GetRsaSecurityKeyXmlAsync(false));

        RSAOAEPKeyExchangeFormatter keyFormatter = new(rsaKey);

        byte[] encryptedSessionKey = keyFormatter.CreateKeyExchange(aes.Key, typeof(Aes));

        byte[] iv = aes.IV;

        // Encrypt the message
        
        PublicKeyEnclosedData<T> envelope = new PublicKeyEnclosedData<T>(data, await myPublicKeySetting.GetRsaSecurityKeyXmlAsync(false));

        string jsonData = JsonSerializer.Serialize(envelope);

        byte[] jsonDataBytes = Encoding.UTF8.GetBytes(jsonData);

        cryptoStream.Write(jsonDataBytes, 0, jsonDataBytes.Length);

        cryptoStream.FlushFinalBlock();

        cryptoStream.Close();

        byte[] encryptedMessage = cipherBytes.ToArray();

        return new EncryptedMessage(iv, encryptedSessionKey, encryptedMessage);
    }

    public async Task<T?> DecryptAsync<T>(EncryptedMessage message, string encryptionKeyName)
    {
        RsaKeySetting recepRsaKeySetting = rsaKeySettingOptions.Get(encryptionKeyName);

        using RSA rsaKey = RSA.Create();
        using Aes aes = Aes.Create();
        rsaKey.FromXmlString(await recepRsaKeySetting.GetRsaSecurityKeyXmlAsync(true));

        RSAOAEPKeyExchangeDeformatter keyDeformatter = new(rsaKey);

        aes.Key = keyDeformatter.DecryptKeyExchange(message.EncryptedSessionKey);

        aes.IV = message.IV;

        // Decrypt the message

        using MemoryStream plainTextBytes = new();
        using CryptoStream cryptoStream = new(plainTextBytes, aes.CreateDecryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(message.EncryptedMessageBytes, 0, message.EncryptedMessageBytes.Length);

        cryptoStream.Close();

        string jsonData = Encoding.UTF8.GetString(plainTextBytes.ToArray());

        return JsonSerializer.Deserialize<T>(jsonData);

    }
}

public record struct PublicKeyEnclosedData<T>(T Data, string PublicKeyXml)
{
    //DataType = typeof(T).ToString(),
    //DataContent = data,
    //PublicKeyXml = "somepublicKeyXml",

}