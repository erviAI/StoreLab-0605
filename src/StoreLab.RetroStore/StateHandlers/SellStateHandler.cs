using StoreLab.ApplicationCore.Models;
using StoreLab.RetroStore.Enums;
using StoreLab.RetroStore.Utils;
using StoreLab.RetroStore.ViewModels;
using StoreLab.RetroStore.Views;

namespace StoreLab.RetroStore.StateHandlers;

public class SellStateHandler : IConsoleStateHandler
{
    // View for displaying the basket and input options
    private static readonly SellView SellView = new SellView();

    public async Task<ConsoleStateResult<object?>> HandleAsync(ConsoleApp context)
    {
        CommonActions options = CommonActions.SearchCatalog | CommonActions.Help | CommonActions.ToggleSellOrPay | CommonActions.Sell;
        var viewModel = new SellViewModel(context.Basket, options);
        SellView.Render(viewModel);

        var inputResult = Utils.UserInputHelper.ReadUserInputAction(options);
        if (inputResult.EscPressed)
        {
            return new ConsoleStateResult<object?>(ConsoleState.Help);
        }

        CommonActions action = inputResult.Action;
        string? typedInput = inputResult.TypedInput;
        int selectedItem = 0;
        if (int.TryParse(typedInput, out int parsedItem) && parsedItem > 0)
        {
            selectedItem = parsedItem;
        }

        switch (action)
        {
            case CommonActions.Help:
                return new ConsoleStateResult<object?>(ConsoleState.Help);
            case CommonActions.Sell:
                CatalogItem? catalogItem = await context.CatalogService.GetCatalogItemById(selectedItem);
                if (catalogItem == null)
                {
                    SellView.ShowItemNotFound(viewModel, selectedItem);
                    return new ConsoleStateResult<object?>(ConsoleState.Sell);
                }
                context.Basket = await context.AddCatalogItemToBasket(catalogItem);
                SellView.ShowItemAdded(viewModel, catalogItem.Name);
                return new ConsoleStateResult<object?>(ConsoleState.Sell, catalogItem);
            case CommonActions.SearchCatalog:
                if (!string.IsNullOrWhiteSpace(typedInput))
                    return new ConsoleStateResult<object?>(ConsoleState.CatalogSearch, typedInput!);
                break;
            case CommonActions.ToggleSellOrPay:
                return new ConsoleStateResult<object?>(ConsoleState.Pay);
        }
        return new ConsoleStateResult<object?>(ConsoleState.Sell);
    }
}
