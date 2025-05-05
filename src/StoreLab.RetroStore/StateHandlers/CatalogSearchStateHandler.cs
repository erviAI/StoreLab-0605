using StoreLab.ApplicationCore.Models;
using StoreLab.RetroStore.Enums;
using StoreLab.RetroStore.Utils;
using StoreLab.RetroStore.ViewModels;
using StoreLab.RetroStore.Views;

namespace StoreLab.RetroStore.StateHandlers;

public class CatalogSearchStateHandler : IConsoleStateHandler
{
    private readonly CatalogSearchView CatalogSearchView = new CatalogSearchView();
    private List<CatalogItem> LastSearchResults = new List<CatalogItem>();
    // Current searchMode
    private CatalogSearchViewModel.CatalogSearchMode SearchMode = CatalogSearchViewModel.CatalogSearchMode.Search;


    public async Task<ConsoleStateResult<object?>> HandleAsync(ConsoleApp context)
    {
        // Detect first time handling of the CatalogSearchState if this is the first time see if any arguments are passed
        string? initialSearchTem = context.LastStateResult?.Argument?.ToString();
        var isFirstTime = true;
        CommonActions options = CommonActions.SearchCatalog | CommonActions.Help | CommonActions.ToggleSellOrPay;
        var viewModel = new ViewModels.CatalogSearchViewModel(context.Basket, options, LastSearchResults, SearchMode);


        // Loop until a valid input is received // until the user presses Esc
        // or selects an item from the search results   
        while (true)
        {

            // If this is the first time, check if there is an initial search term
            if (isFirstTime && !string.IsNullOrWhiteSpace(initialSearchTem))
            {
                // Perform the search with the initial search term
                await DoSearch(context, viewModel, initialSearchTem);
            }

            options = CommonActions.SearchCatalog | CommonActions.Help | CommonActions.ToggleSellOrPay;

            // toggle CommonActions.Sell | CommonActions.Select depending on the current search mode
            if (SearchMode == CatalogSearchViewModel.CatalogSearchMode.Select)
            {
                options |= CommonActions.Select;
            }
            else
            {
                options |= CommonActions.Sell;
            }


            viewModel = new CatalogSearchViewModel(context.Basket, options, LastSearchResults, SearchMode);
            CatalogSearchView.Render(viewModel);


            var inputResult = UserInputHelper.ReadUserInputAction(options);
            if (inputResult.EscPressed)
            {
                // User pressed Esc, return to the previous state (Sell)
                return new ConsoleStateResult<object?>(ConsoleState.Sell, null);
            }

            if (inputResult.TypedInput != null && inputResult.Action == CommonActions.SearchCatalog)
            {
                // User wants to search the catalog
                string searchInput = inputResult.TypedInput;
                await DoSearch(context, viewModel, searchInput);
            }
            else if (inputResult.Action == CommonActions.Select)
            {
                if (inputResult.OptionNumber >= 1 && inputResult.OptionNumber <= LastSearchResults.Count)
                {
                    CatalogItem selectedCatalogItem = LastSearchResults[inputResult.OptionNumber - 1];
                    context.Basket = await context.AddCatalogItemToBasket(selectedCatalogItem);
                    CatalogSearchView.ShowItemAdded(selectedCatalogItem.Name);
                    LastSearchResults = new List<CatalogItem>();
                    SearchMode = CatalogSearchViewModel.CatalogSearchMode.Search;
                    return new ConsoleStateResult<object?>(ConsoleState.Sell, selectedCatalogItem);
                }
                else
                {
                    CatalogSearchView.ShowInvalidSelection(inputResult.OptionNumber);
                }
            }
            else if (inputResult.Action == CommonActions.ToggleSellOrPay)
            {
                var nextState = context.CurrentState == ConsoleState.Pay ? ConsoleState.Sell : ConsoleState.Pay;
                return new ConsoleStateResult<object?>(nextState, null);
            }

            isFirstTime = false;
        }
    }

    private async Task DoSearch(ConsoleApp context, CatalogSearchViewModel viewModel, string searchInput)
    {
        LastSearchResults = await context.CatalogService.Search(searchInput);

        if (LastSearchResults.Count > 20)
        {
            CatalogSearchView.ShowTooManyResults();
            SearchMode = CatalogSearchViewModel.CatalogSearchMode.Search;
        }
        else if (LastSearchResults.Count == 0)
        {
            CatalogSearchView.ShowNoResults(viewModel);
            SearchMode = CatalogSearchViewModel.CatalogSearchMode.Search;
        }
        else
        {
            // Set the search mode to Select
            SearchMode = CatalogSearchViewModel.CatalogSearchMode.Select;
        }
    }
}
