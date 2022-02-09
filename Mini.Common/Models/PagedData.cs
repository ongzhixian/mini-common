namespace Mini.Common.Models;

public readonly record struct PagedData<T>
{
    public long TotalRecordCount { get; init; }

    public IEnumerable<T> Data { get; init; }
}