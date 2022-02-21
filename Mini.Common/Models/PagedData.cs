namespace Mini.Common.Models;

public readonly record struct PagedData<T>(
    uint Page,
    uint PageSize,
    ulong TotalRecordCount,
    IEnumerable<T> Data
    );

#pragma warning disable S125 // Sections of code should not be commented out

// This is the same thing as the above.
// Note that in order for it to be deserialize, it requires the constructor.
/*
public readonly record struct PagedData<T>
{
    public uint Page { get; init; } = 1;
    public uint PageSize { get; init; } = 12;
    public ulong TotalRecordCount { get; init; } = 0;
    public IEnumerable<T> Data { get; init; } = new List<T>();

    public PagedData(uint page, uint pageSize, ulong totalRecordCount, IEnumerable<T> data)
    {
        (Page, PageSize, TotalRecordCount, Data) = (page, pageSize, totalRecordCount, data);
    }
}
*/

#pragma warning restore S125 // Sections of code should not be commented out
