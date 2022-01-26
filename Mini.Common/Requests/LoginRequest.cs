namespace Mini.Common.Requests;

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Username:{Username}, Password:{Password}";
    }
}
