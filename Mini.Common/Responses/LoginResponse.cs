namespace Mini.Common.Responses;

[ExcludeFromCodeCoverage]
public class LoginResponse
{
    public string Jwt { get; set; } = string.Empty;

    public DateTime ExpiryDateTime { get; set; }
}
