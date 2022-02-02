namespace Mini.Wms;

public interface ICustomer
{
    string Id { get; init; }

    string Name { get; init; }

    DateTime CreatedDateTime { get; init; }
}

public readonly record struct Customer(string Id, string Name, DateTime CreatedDateTime) : ICustomer
{
    public override string ToString() =>
        $"Id:{Id}, Name:{Name}, CreatedDateTime:{CreatedDateTime:s}";

}
