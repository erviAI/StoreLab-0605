using StoreLab.ApplicationCore.Models;
using StoreLab.RetroStore.Enums;
using StoreLab.RetroStore;

namespace StoreLab.RetroStore.Utils;

public static partial class PrintHelper
{
    // Prints the sticky state header at line 1 (second line)
    private static void PrintStateHeader(string stateText)
    {
        // Add 5 char padding on each side of the state string
        string stateStr = $"     {stateText}     ";
        int totalWidth = Console.WindowWidth - 1;
        int stateLen = stateStr.Length;
        int pad = (totalWidth - stateLen) / 2;
        if (pad < 0) pad = 0;
        string beforeState = new string('=', pad);
        string afterState = new string('=', totalWidth - pad - stateLen);
        Console.SetCursorPosition(0, 1);
        Console.Write(beforeState);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(stateStr);
        Console.ResetColor();
        Console.WriteLine(afterState);
    }

    // Helper to print a labeled value section at the bottom of the console
    public static void PrintBottomSection(string label, string value)
    {
        int sepWidth = Console.WindowWidth - 1;
        if (sepWidth < 1) sepWidth = 20;
        string sep = new string('-', sepWidth);
        int totalConsoleLines = Console.WindowHeight;
        int totalSectionLines = 3; // sep, label+value, sep
        int totalSectionStart = totalConsoleLines - totalSectionLines - 3; // 2 more lines up, like Sell/Pay
        Console.SetCursorPosition(0, totalSectionStart);
        Console.WriteLine(sep);
        Console.SetCursorPosition(0, totalSectionStart + 1);
        int labelLen = label.Length;
        int valueLen = value.Length;
        int spaceBetween = sepWidth - labelLen - valueLen;
        if (spaceBetween < 1) spaceBetween = 1;
        Console.Write(label + new string(' ', spaceBetween));
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(value);
        Console.ResetColor();
        Console.SetCursorPosition(0, totalSectionStart + 2);
        Console.WriteLine(sep);
    }

    // Helper to distribute remaining width among columns with Width == 0
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

    // Generic table printer for any row type
    public static void PrintTable<T>(List<T> rows, List<TableColumn<T>> columns)
    {
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

    public static void PadLines(int linesUsed, int totalLines = 16)
    {
        for (int i = linesUsed; i < totalLines; i++)
        {
            Console.WriteLine();
        }
    }

    public static void WriteInputOptions(CommonActions options)
    {
        // Clear the console and reset the cursor position
        Console.Clear();
        Console.SetCursorPosition(0, 0);

        PrintStateHeader("Options");

        // Prepare options as key-description pairs
        var optionList = new List<InputOptionRow>();
        if (options.HasFlag(CommonActions.Help))
            optionList.Add(new InputOptionRow { Key = "?", Description = "Show this help" });
        if (options.HasFlag(CommonActions.SearchCatalog))
            optionList.Add(new InputOptionRow { Key = "s", Description = "New search" });
        if (options.HasFlag(CommonActions.ToggleSellOrPay))
            optionList.Add(new InputOptionRow { Key = "Tab", Description = "Switch mode (sell/buy)" });
        if (options.HasFlag(CommonActions.Quit))
            optionList.Add(new InputOptionRow { Key = "q", Description = "Quit" });
        if (options.HasFlag(CommonActions.Select))
            optionList.Add(new InputOptionRow { Key = "#", Description = "Select a list item" });

        // Use TableColumn and PrintTable for output
        var columns = new List<TableColumn<InputOptionRow>>
        {
            new TableColumn<InputOptionRow>("", 32, (row, idx) => ""),
            new TableColumn<InputOptionRow>("Key", 8, (row, idx) => row.Key),
            new TableColumn<InputOptionRow>("Description", 0, (row, idx) => row.Description)
        };
        DistributeTableColumnWidths(columns);
        PrintTable(optionList, columns);

        AsciiArtPrinter(Constants.AsciiArt);
    }

    public static void AsciiArtPrinter(string asciiArt)
    {
        // Place the ascii art so its bottom is 5 lines from the bottom of the console
        int totalWidth = Console.WindowWidth - 1;
        var asciiLines = asciiArt.Split('\n');
        int artHeight = asciiLines.Length;
        int startLine = Console.WindowHeight - 5 - artHeight;
        for (int i = 0; i < asciiLines.Length; i++)
        {
            string line = asciiLines[i];
            int pad = (totalWidth - line.Length) / 2;
            if (pad < 0) pad = 0;
            string before = new string(' ', pad);
            Console.SetCursorPosition(0, startLine + i);
            Console.Write(before + line);
        }
    }

    public static void WriteInputOptionsAtBottom(CommonActions options, bool includeEsc = false)
    {
        // Always print the options+version on the last line of the console
        int lastLine = Console.WindowHeight - 1;
        Console.SetCursorPosition(0, lastLine);
        // Clear the line
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, lastLine);
        // Tab-style options
        List<string> tabs = new List<string>();
        if (options.HasFlag(CommonActions.Help))
            tabs.Add("[ ? ]");
        if (options.HasFlag(CommonActions.SearchCatalog))
            tabs.Add("[ [A-Za-z] - to search ]");
        if (options.HasFlag(CommonActions.ToggleSellOrPay))
            tabs.Add("[ tab - to toogle sell/pay]");
        if (options.HasFlag(CommonActions.Quit))
            tabs.Add("[ q ]");
        if (options.HasFlag(CommonActions.Select))
             tabs.Add("[ # ]");
        if (includeEsc)
            tabs.Add("[ esc ]");
        string tabsLine = tabs.Count > 0 ? string.Join(" ", tabs) : string.Empty;
        string version = "Version 0.1.0";
        int totalWidth = Console.WindowWidth - 1;
        int tabsLen = tabsLine.Length;
        int versionLen = version.Length;
        int space = totalWidth - tabsLen - versionLen;
        if (space < 1) space = 1;
        Console.Write(tabsLine + new string(' ', space) + version);
    }

    public static void WritePromptAboveTabs(string prompt)
    {
        int promptLine = Console.WindowHeight - 2;
        Console.SetCursorPosition(0, promptLine);
        // Clear the line
        Console.Write(new string(' ', Console.WindowWidth - 1));
        Console.SetCursorPosition(0, promptLine);
        Console.Write(prompt);
    }
}    // Helper class for input option rows