using StoreLab.RetroStore.Enums;
using StoreLab.RetroStore.Views;
using StoreLab.RetroStore.ViewModels;

namespace StoreLab.RetroStore.StateHandlers;

public class HelpStateHandler : IConsoleStateHandler
{
    private readonly HelpView HelpView = new HelpView();
    public async Task<ConsoleStateResult<object?>> HandleAsync(ConsoleApp context)
    {
        CommonActions options = CommonActions.SearchCatalog | CommonActions.Help | CommonActions.Quit | CommonActions.ToggleSellOrPay | CommonActions.Sell;
        var viewModel = new HelpViewModel(options);
        HelpView.Render(viewModel);

        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Q)
                {
                    return new ConsoleStateResult<object?>(ConsoleState.Quit);
                }
                else if (key.Key == ConsoleKey.Oem2 || key.KeyChar == '?')
                {
                    return new ConsoleStateResult<object?>(ConsoleState.Help);
                }
                else if (key.Key == ConsoleKey.S)
                {
                    return new ConsoleStateResult<object?>(ConsoleState.CatalogSearch);
                }
                else if (key.Key == ConsoleKey.Tab)
                {
                    return new ConsoleStateResult<object?>(ConsoleState.Sell);
                }
                else if (char.IsDigit(key.KeyChar))
                {
                    return new ConsoleStateResult<object?>(ConsoleState.Sell);
                }
            }
            await Task.Delay(10);
        }
    }
}