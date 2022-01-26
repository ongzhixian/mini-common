namespace Mini.Common.Responses;

public class LoginResponse
{
    public string Jwt { get; set; } = string.Empty;

    public DateTime ExpiryDateTime { get; set; }

    public override string ToString()
    {
        return $"Jwt:{Jwt}, ExpiryDateTime:{ExpiryDateTime}";
    }
}
