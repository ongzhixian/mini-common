namespace Mini.Wms.Abstraction.Models;

public interface IObject<T>
{
    T Id { get; init; }
    DateTime CreatedDateTime { get; init; }
    DateTime LastUpdatedDateTime { get; init; }
}

public interface IUser<T> : IObject<T>
{
    string Username { get; init; }
    string FirstName { get; init; }
    string LastName { get; init; }
    string Password { get; init; }
    DateTime PasswordUpdatedDateTime { get; init; }
}

public interface ICustomer<T> : IObject<T>
{
    string Name { get; init; }
}