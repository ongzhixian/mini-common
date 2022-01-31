namespace Mini.Common.Responses;

public readonly record struct LoginResponse(string Jwt, DateTime ExpiryDateTime)
{
    public override string ToString() =>
        $"Jwt:{Jwt}, ExpiryDateTime:{ExpiryDateTime:s}";
}

#pragma warning disable S125 // Sections of code should not be commented out
/*
 * Aside: The above code is equivalent to the following:
 * We are avocating using readonly struct because there are 
 * commentary stating that `record struct` are faster than plain `struct`

public readonly struct LoginResponse : IEquatable<LoginResponse>
{
    public LoginResponse(string Jwt, DateTime ExpiryDateTime);

    public string Jwt { get; init; }

    public DateTime ExpiryDateTime { get; init; }

    public override string ToString();

    public static bool operator !=(LoginResponse left, LoginResponse right);

    public static bool operator ==(LoginResponse left, LoginResponse right);

    public override int GetHashCode();

    public override bool Equals(
    #nullable disable
    object obj);

    public bool Equals(LoginResponse other);

    public void Deconstruct(out 
    #nullable enable
    string Jwt, out DateTime ExpiryDateTime);
}

Compared to a class implementation:

public class LoginResponse
{
    public string Jwt { get; set; } = string.Empty;

    public DateTime ExpiryDateTime { get; set; }

    public override string ToString()
    {
        return $"Jwt:{Jwt}, ExpiryDateTime:{ExpiryDateTime:s}";
    }
}

A pure struct implementation

public readonly struct LoginResponse
{
    public readonly string Jwt { get; init; } = string.Empty;

    public readonly DateTime ExpiryDateTime { get; init; }

    [JsonConstructor]
    public LoginResponse(string jwt, DateTime expiryDateTime)
    {
        this.Jwt = jwt;
        this.ExpiryDateTime = expiryDateTime;
    }

    public override string ToString() =>
        $"Jwt:{Jwt}, ExpiryDateTime:{ExpiryDateTime:s}";
}

*/
#pragma warning restore S125 // Sections of code should not be commented out