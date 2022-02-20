using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Mini.Wms.Abstraction.Models;

public readonly record struct PagedData<T>
{
    public ulong TotalRecordCount { get; init; } = 0;

    public IEnumerable<T>? Data { get; init; } = null;

    public uint Page { get; init; } = 1;

    public uint PageSize { get; init; } = 12;
}

public class PagedDataOptions
{
    public uint Page { get; set; }
    public uint PageSize { get; set; }
    public string DataType { get; set; }

    public IList<DataField> DataFieldList { get; set; }
    public IList<DataFilter> DataFilterList { get; set; }

    public PagedDataOptions()
    {
        Page = 1;
        PageSize = 12;
        DataType = string.Empty;
        DataFieldList = new List<DataField>();
        DataFilterList = new List<DataFilter>();
    }

    public PagedDataOptions(IQueryCollection query) : this()
    {
        if (query.ContainsKey("page") && uint.TryParse(query["page"], out uint page))
        {
            Page = page;
        }

        if (query.ContainsKey("page-size") && uint.TryParse(query["page-size"], out uint pageSize))
        {
            PageSize = pageSize;
        }

        if (query.ContainsKey("fields"))
        {
            foreach (var sv in query["fields"])
            {
                if (!string.IsNullOrWhiteSpace(sv))
                {
                    DataFieldList.Add(new DataField(sv));
                }
            }
        }
    }

    public string ToQueryString()
    {
        IList<KeyValuePair<string, StringValues>> qs = new List<KeyValuePair<string, StringValues>>
        {
            new KeyValuePair<string, StringValues>("page", Page.ToString()),
            new KeyValuePair<string, StringValues>("page-size", PageSize.ToString()),
            new KeyValuePair<string, StringValues>("fields", new StringValues(DataFieldList.Select(r => r.ToString()).ToArray()))
        };

        return QueryString.Create(qs).Value;
    }
}

public class DataField
{
    public string Name { get; set; } = string.Empty;

    public bool SortAscending { get; set; }

    public int SortOrder { get; set; }

    public DataField()
    {
    }

    public DataField(string Name, bool SortAscending, int SortOrder)
    {
        this.Name = Name;
        this.SortAscending = SortAscending;
        this.SortOrder = SortOrder;
    }

    public DataField(string sv)
    {
        var parts = sv.Split(',', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 2)
        {
            return;
        }

        Name = parts[0];

        if (bool.TryParse(parts[1], out bool sortAscending))
        {
            SortAscending = sortAscending;
        }

        if (int.TryParse(parts[2], out int sortOrder))
        {
            SortOrder = sortOrder;
        }
    }

    public override string ToString()
    {
        return $"{Name},{SortAscending},{SortOrder}";
    }
}

public class DataFilter
{
}