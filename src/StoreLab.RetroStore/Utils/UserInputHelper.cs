using StoreLab.RetroStore.Enums;

namespace StoreLab.RetroStore.Utils;

public record UserInputResult(CommonActions Action, int OptionNumber, string? TypedInput, bool EscPressed, bool TabPressed);

public struct UserInputRawResult
{
    public string Text { get; set; }
    public bool EscPressed { get; set; }
    public bool TabPressed { get; set; }
    public bool EnterPressed { get; set; }
}

public static class UserInputHelper
{
    public static UserInputRawResult GetRawUserInput()
    {
        int promptLine = Console.WindowHeight - 2;
        int notificationLine = Console.WindowHeight - 3;
        int cursorLeft = 0;
        int cursorTop = promptLine;
        var sb = new System.Text.StringBuilder();
        string blank = new string(' ', Console.WindowWidth - 1);
        int maxInputLen = Console.WindowWidth - 1;
        bool notificationCleared = false;
        while (true)
        {
            Console.SetCursorPosition(cursorLeft, cursorTop);
            var k = Console.ReadKey(intercept: true);
            if (!notificationCleared && k.Key != ConsoleKey.Enter && k.Key != ConsoleKey.Tab && k.Key != ConsoleKey.Escape && k.Key != ConsoleKey.Backspace)
            {
                Console.SetCursorPosition(0, notificationLine);
                Console.Write(blank);
                notificationCleared = true;
            }
            if (k.Key == ConsoleKey.Enter)
            {
                return new UserInputRawResult { Text = sb.ToString(), EnterPressed = true };
            }
            else if (k.Key == ConsoleKey.Backspace)
            {
                if (sb.Length > 0)
                    sb.Length--;
            }
            else if (k.Key == ConsoleKey.Tab)
            {
                return new UserInputRawResult { Text = sb.ToString(), TabPressed = true };
            }
            else if (k.Key == ConsoleKey.Escape)
            {
                return new UserInputRawResult { Text = sb.ToString(), EscPressed = true };
            }
            else if (k.KeyChar != '\0' && sb.Length < maxInputLen)
            {
                sb.Append(k.KeyChar);
            }
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(blank);
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(sb.ToString());
        }
    }

    public static UserInputResult ReadUserInputAction(CommonActions options)
    {
        int optionNumber = 0;
        bool escPressed = false;
        bool tabPressed = false;
        bool enterPressed = false;
        string? typedInput = null;
        CommonActions action;
        do
        {
            var raw = GetRawUserInput();
            escPressed = raw.EscPressed;
            tabPressed = raw.TabPressed;
            if (escPressed)
            {
                return new UserInputResult(CommonActions.SearchCatalog, 0, null, true, false);
            }
            if (raw.TabPressed || raw.EnterPressed && string.IsNullOrWhiteSpace(raw.Text))
            {
                typedInput = "tabby";
            }
            else if (raw.EnterPressed)
            {
                typedInput = raw.Text;
                enterPressed = true;
            }
            action = typedInput switch
            {
                "q" when options.HasFlag(CommonActions.Quit) => CommonActions.Quit,
                "?" when options.HasFlag(CommonActions.Help) => CommonActions.Help,
                "s" when options.HasFlag(CommonActions.SearchCatalog) => CommonActions.SearchCatalog,
                "tabby" when options.HasFlag(CommonActions.ToggleSellOrPay) => CommonActions.ToggleSellOrPay,
                _ when int.TryParse(typedInput, out optionNumber) => options.HasFlag(CommonActions.Sell) ? CommonActions.Sell : CommonActions.Select,
                _ => options.HasFlag(CommonActions.SearchCatalog) ? CommonActions.SearchCatalog : CommonActions.Repeat
            };
        } while (action == CommonActions.Repeat && !escPressed && !enterPressed);
        return new UserInputResult(action, optionNumber, typedInput, escPressed, tabPressed);
    }
}