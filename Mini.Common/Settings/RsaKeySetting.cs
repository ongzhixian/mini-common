using System.Net.Http;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Mini.Common.Settings;

public record class RsaKeySetting
{
    public RsaKeyDataSource SourceType { get; init; } = RsaKeyDataSource.Unknown;

    public string Source { get; init; } = string.Empty;

    private readonly HttpClient httpClient;

    public RsaKeySetting()
    {
        httpClient = new HttpClient();
    }

    public RsaKeySetting(HttpClient? httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<string> SourceXmlAsync()
    {
        string rsaXml = string.Empty;

        if (SourceType == RsaKeyDataSource.EnvironmentVariable)
        {
            rsaXml = Environment.GetEnvironmentVariable(Source) ?? rsaXml;
        }

        if (SourceType == RsaKeyDataSource.File && File.Exists(Source))
        {
            rsaXml = File.ReadAllText(Source);
        }

        if (SourceType == RsaKeyDataSource.Http)
        {
            rsaXml = await this.httpClient.GetStringAsync(Source);
        }

        return rsaXml;
    }

    public void EnsureIsValid()
    {
        if (SourceType == RsaKeyDataSource.Unknown)
            throw new InvalidOperationException("SourceType is unknown");

        if (string.IsNullOrWhiteSpace(Source))
            throw new InvalidOperationException("Source is null or whitespace");
    }

    public async Task<RsaSecurityKey> GetRsaSecurityKeyAsync(bool withPrivateParameters)
    {
        using RSA rsa = RSA.Create();

        rsa.FromXmlString(await SourceXmlAsync());

        return new RsaSecurityKey(rsa.ExportParameters(withPrivateParameters));
    }

    public async Task<string> GetRsaSecurityKeyXmlAsync(bool withPrivateParameters)
    {
        using RSA rsa = RSA.Create();

        rsa.FromXmlString(await SourceXmlAsync());

        return rsa.ToXmlString(withPrivateParameters);
    }

    public enum RsaKeyDataSource
    {
        Unknown = 0,
        EnvironmentVariable = 1,
        File = 2,
        Http = 3
    }
}

