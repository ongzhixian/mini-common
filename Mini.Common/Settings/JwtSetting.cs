namespace Mini.Common.Settings;

public class JwtSetting
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationMinutes { get; set; } = 20;

    public void EnsureIsValid()
    {
        if (string.IsNullOrWhiteSpace(Issuer))
            throw new InvalidDataException("Issuer is null or whitespace.");

        if (string.IsNullOrWhiteSpace(Audience))
            throw new InvalidDataException("Audience is null or whitespace.");
    }

    public override string ToString()
    {
        return $"Issuer:{Issuer}, Audience:{Audience}, ExpirationMinutes:{ExpirationMinutes}";
    }
}