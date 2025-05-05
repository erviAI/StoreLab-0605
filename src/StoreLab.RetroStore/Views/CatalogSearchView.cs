using StoreLab.ApplicationCore.Models;
using StoreLab.RetroStore.Utils;
using StoreLab.RetroStore.ViewModels;

namespace StoreLab.RetroStore.Views;

public class CatalogSearchView : ViewBase<CatalogSearchViewModel, CatalogSearchViewModelActions>
{
    public override string GetPromptText(CatalogSearchViewModel viewModel)
    {
        return viewModel.Mode == CatalogSearchViewModel.CatalogSearchMode.Select ? "(Select #) " : "(Search) ";
    }

    public override string GetHeaderText(CatalogSearchViewModel viewModel)
    {
        return $"Search results: {viewModel.SearchCount} items found";
    }

    public override void RenderInternal(CatalogSearchViewModel viewModel)
    {
        var columns = new List<TableColumn<CatalogItem>>
        {
            new TableColumn<CatalogItem>("#", 3, (item, idx) => (idx + 1).ToString()),
            new TableColumn<CatalogItem>("[ID]", 6, (item, idx) => item.Id.ToString()),
            new TableColumn<CatalogItem>("Name", 16, (item, idx) => item.Name ?? ""),
            new TableColumn<CatalogItem>("Desc", 0, (item, idx) => item.Description ?? ""),
            new TableColumn<CatalogItem>("Stock", 8, (item, idx) => item.AvailableStock.ToString(), alignRight: true),
            new TableColumn<CatalogItem>("Price", 8, (item, idx) => item.Price.ToString(), alignRight: true)
        };
        TableColumnHelper.PrintTable(viewModel.Items ?? new List<CatalogItem>(), columns);
        string label = "Search count:";
        string value = viewModel.SearchCount.ToString();
        PrintBottomSection(label, value);
    }

    public void ShowTooManyResults()
    {
        NotificationHelper.ShowNotification("More than 20 items satisfy the search, please provide more specific input...", ConsoleColor.Yellow);
    }

    public void ShowNoResults(CatalogSearchViewModel viewModel)
    {
        AddViewData(viewModel, CatalogSearchViewModelActions.NoResult, ViewDataMode.Warning, "No matching items found.");
    }

    public void ShowInvalidSelection(int optionNumber)
    {
        NotificationHelper.ShowNotification($"Invalid selection: {optionNumber}. Please enter a number from the list above.", ConsoleColor.Red);
    }

    public void ShowItemAdded(string itemName)
    {
        NotificationHelper.ShowNotification($"Added '{itemName}' to basket.", ConsoleColor.Green);
    }
}
