namespace StoreLab.RetroStore.Utils;

public class TableColumn<T>
{
    public string Header { get; set; }
    public int Width { get; set; } // 0 means use remaining width
    public Func<T, int, string> ValueSelector { get; set; }
    public bool AlignRight { get; set; }

    public TableColumn(string header, int width, Func<T, int, string> valueSelector, bool alignRight = false)
    {
        Header = header;
        Width = width;
        ValueSelector = valueSelector;
        AlignRight = alignRight;
    }
}

public class TableColumnHelper  
{

    public static void PrintTable<T>(List<T> rows, List<TableColumn<T>> columns)
    {
        TableColumnHelper.DistributeTableColumnWidths(columns);
        string header = string.Join(" ", columns.Select(c => c.AlignRight ? c.Header.PadLeft(c.Width) : c.Header.PadRight(c.Width)));
        Console.WriteLine(header);
        for (int i = 0; i < rows.Count; i++)
        {
            var item = rows[i];
            string row = string.Join(" ", columns.Select(c =>
            {
                string val = c.ValueSelector(item, i);
                val = Truncate(val, c.Width);
                return c.AlignRight ? val.PadLeft(c.Width) : val.PadRight(c.Width);
            }));
            Console.WriteLine(row);
        }
    }

    private static string Truncate(string value, int width)
    {
        if (string.IsNullOrEmpty(value) || width < 2) return value;
        return value.Length > width ? value.Substring(0, width - 1) + "…" : value;
    }
    public static void DistributeTableColumnWidths<T>(List<TableColumn<T>> columns)
    {
        int totalWidth = Console.WindowWidth - 1;
        int totalFixed = columns.Where(c => c.Width > 0).Sum(c => c.Width) + (columns.Count - 1); // spaces between columns
        int numFlexible = columns.Count(c => c.Width == 0);
        int remaining = totalWidth - totalFixed;
        int flexibleWidth = numFlexible > 0 ? Math.Max(2, remaining / numFlexible) : 0;
        foreach (var col in columns)
        {
            if (col.Width == 0)
                col.Width = flexibleWidth;
        }
    }
}
