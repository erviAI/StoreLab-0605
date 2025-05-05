using StoreLab.RetroStore.Enums;
using StoreLab.RetroStore.Utils;
using StoreLab.RetroStore.ViewModels;

namespace StoreLab.RetroStore.Views;

public abstract class ViewBase<T, TE> where T : IViewModel<TE> where TE : Enum
{
    public void Render(T ViewModel)
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);

        // Print the header
        string headerText = GetHeaderText(ViewModel);
        if (!string.IsNullOrWhiteSpace(headerText))
        {
            PrintStateHeader(headerText);
        }

        // Print the state header
        //;
        // Render the internal view
        RenderInternal(ViewModel);
        // Print the bottom section with the state

        ShowViewData(ViewModel);

        // Empty line for better readability
        Console.WriteLine();

        RenderPrompt(ViewModel);

        RenderInputOptions(ViewModel.Actions);

        ViewModel.ResetViewData(); // Reset the view data after showing it
    }

    // Abstract method to get what to show in the prompt
    public abstract string GetPromptText(T viewModel);
    public abstract string GetHeaderText(T viewModel);
    
    // Method to add view data
    public void AddViewData(T viewModel, TE viewDataType, ViewDataMode viewDataMode, string message)
    {
        viewModel.ViewData = new Tuple<TE, ViewDataMode, object?>(viewDataType, viewDataMode, message);
    }


    // Method to show the view data
    protected virtual void ShowViewData(T viewModel)
    {
        // Show data from ViewData
        if (viewModel.ViewData != null)
        {
            switch (viewModel.ViewData.Item1)
            {
                default:
                    // Handle other cases if needed
                    NotificationHelper.ShowNotification($"{viewModel.ViewData.Item3}", 
                        NotificationColor(viewModel));
                    break;  
            }
        }
    }

    private ConsoleColor NotificationColor(T viewModel)
    {
        return viewModel.ViewData.Item2 switch
        {
            // Green for success, Red for error, Yellow for warning
            ViewDataMode.Info => ConsoleColor.Green,
            ViewDataMode.Warning => ConsoleColor.Yellow,
            ViewDataMode.Error => ConsoleColor.Red,
            _ => ConsoleColor.White
        };
    }

    public virtual void RenderPrompt(T viewModel)
    {
        var promptText = GetPromptText(viewModel);
        if (string.IsNullOrWhiteSpace(promptText)) return;
        
        PrintHelper.WritePromptAboveTabs(promptText);
    }
    // Abstract method to be implemented by derived classes
    public abstract void RenderInternal(T viewModel);

    public void RenderInputOptions(CommonActions options)
    {
        PrintHelper.WriteInputOptionsAtBottom(options);
    }

    public void PrintStateHeader(string stateText)
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

    // Helper to distribute remaining width among columns with Width == 0
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
}