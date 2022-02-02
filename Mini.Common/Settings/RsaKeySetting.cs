using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Mini.Common.Settings;

public record class RsaKeySetting
{
    public RsaKeyDataSource SourceType { get; init; } = RsaKeyDataSource.Default;

    public string Source { get; init; } = string.Empty;

    public string RsaXml()
    {
        string rsaXml = string.Empty;

        if (SourceType == RsaKeyDataSource.EnvironmentVariable)
        {
            rsaXml = Environment.GetEnvironmentVariable(Source) ?? rsaXml;
        }

        if (SourceType == RsaKeyDataSource.File)
        {
            if (!File.Exists(Source))
                throw new FileNotFoundException("File not found", Source);

            rsaXml = File.ReadAllText(Source);
        }

        return rsaXml;
    }

    public RsaSecurityKey GetRsaSecurityKey(bool withPrivateParameters)
    {
        using RSA rsa = RSA.Create();

        rsa.FromXmlString(this.RsaXml());

        return new RsaSecurityKey(rsa.ExportParameters(withPrivateParameters));
    }

    public enum RsaKeyDataSource
    {
        Default = 0,
        EnvironmentVariable = 1,
        File = 2
    }
}

