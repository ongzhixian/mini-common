namespace Mini.Common.Models;

public readonly record struct PagedData<T>
{
    public ulong TotalRecordCount { get; init; } = 0;

    public IEnumerable<T>? Data { get; init; } = null;

    public uint Page { get; init; } = 1;

    public uint PageSize { get; init; } = 12;
}