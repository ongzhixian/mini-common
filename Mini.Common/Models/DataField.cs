namespace Mini.Common.Models;

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