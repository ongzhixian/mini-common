namespace Mini.Wms.DomainMessages;
public readonly record struct AddUser
{
    public string Username { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;
}

public abstract record NewUser
{
    private string Username { get; init; } = string.Empty;
    private string FirstName { get; init; } = string.Empty;
    private string LastName { get; init; } = string.Empty;

}
