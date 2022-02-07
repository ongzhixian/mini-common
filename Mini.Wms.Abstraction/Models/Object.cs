namespace Mini.Wms.Abstraction.Models;

public interface IDbObject<T>
{
    T Id { get; init; }
    DateTime CreatedDateTime { get; init; }
    DateTime LastUpdatedDateTime { get; init; }
}

public interface IUser<T> : IDbObject<T>, IUser
{
    DateTime PasswordUpdatedDateTime { get; init; }
}

public interface IUser
{
    string Username { get; init; }
    string FirstName { get; init; }
    string LastName { get; init; }
    string Password { get; init; }
}

public class User : IUser
{
    public string Username { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public interface ICustomer<T> : IDbObject<T>, ICustomer
{
    string Name { get; init; }
}

public interface ICustomer
{
    string Name { get; init; }
}