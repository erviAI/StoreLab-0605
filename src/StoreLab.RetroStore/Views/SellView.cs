using StoreLab.ApplicationCore.Models;
using StoreLab.RetroStore.Utils;
using StoreLab.RetroStore.ViewModels;

namespace StoreLab.RetroStore.Views;

public class SellView : ViewBase<SellViewModel, SellViewModelActions>
{
    public override string GetPromptText(SellViewModel viewModel)
    {
        return "(Sell) ";
    }
    public override string GetHeaderText(SellViewModel viewModel)
    {
        return $"State: {viewModel?.State}";
    }

    public override void RenderInternal(SellViewModel viewModel)
    {
        //PrintStateHeader($"State: {viewModel?.State}");

        // Define columns for the table
        var columns = new List<TableColumn<BasketItem>>
        {
            new TableColumn<BasketItem>("#", 3, (item, idx) => (idx + 1).ToString()),
            new TableColumn<BasketItem>("Id", 6, (item, idx) => item.Id.ToString()),
            new TableColumn<BasketItem>("Name", 16, (item, idx) => item.Name ?? ""),
            new TableColumn<BasketItem>("Desc", 0, (item, idx) => item.Description ?? ""),
            new TableColumn<BasketItem>("Qty", 5, (item, idx) => item.Quantity.ToString(), alignRight: true),
            new TableColumn<BasketItem>("Price", 8, (item, idx) => item.Price.ToString(), alignRight: true),
            new TableColumn<BasketItem>("Total", 10, (item, idx) => (item.Price * item.Quantity).ToString(), alignRight: true)
        };

        // Prepare item lines
        List<BasketItem> items = viewModel?.Items ?? new List<BasketItem>();
        TableColumnHelper.PrintTable(items, columns);
        string label = "Total amount:";
        string value = $"{viewModel?.TotalAmount ?? 0}";
        // Print total section 2 lines further up
        PrintBottomSection(label, value);
    }

    public void ShowItemNotFound(SellViewModel viewModel, int itemId)
    {
        // Add view data for item not found
        AddViewData(viewModel, SellViewModelActions.ShowItemNotFound, ViewDataMode.Warning, $"Item #{itemId} not found in catalog. Please try again.");
        Render(viewModel);
    }

    public void ShowItemAdded(SellViewModel viewModel, string itemName)
    {
        // Add view data for item added
        AddViewData(viewModel, SellViewModelActions.ShowItemAdded, ViewDataMode.Info, $"Added '{itemName}' to basket.");
        //NotificationHelper.ShowNotification($"Added '{itemName}' to basket.", ConsoleColor.Green);
        Render(viewModel);
    }
}
