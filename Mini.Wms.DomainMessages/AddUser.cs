namespace Mini.Wms.DomainMessages;
public readonly record struct AddUser
{
    public string Username { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;
}

public abstract record NewUser
{
    public virtual string Username { get; init; } = string.Empty;
    public virtual string FirstName { get; init; } = string.Empty;
    public virtual string LastName { get; init; } = string.Empty;

}
