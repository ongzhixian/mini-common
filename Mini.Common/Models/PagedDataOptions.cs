using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Mini.Common.Models;

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
                DataFieldList.Add(new DataField(sv));
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