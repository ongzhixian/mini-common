namespace Mini.Common.Requests;


public readonly record struct LoginRequest(string Username, string Password)
{
    public override string ToString() =>
        $"Username:{Username}, Password:{Password}";
}