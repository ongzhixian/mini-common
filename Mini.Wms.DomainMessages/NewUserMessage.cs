namespace Mini.Wms.DomainMessages;

public interface INewUserMessage
{
    string Username { get; init; }
    string FirstName { get; init; }
    string LastName { get; init; }

}

public readonly record struct NewUserMessage(string Username, string FirstName, string LastName) : INewUserMessage;