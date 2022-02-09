namespace Mini.Common.Models;

public interface IObject<T>
{
    T? Id { get; init; }
    DateTime CreatedDateTime { get; init; }
    DateTime LastUpdatedDateTime { get; init; }
}
