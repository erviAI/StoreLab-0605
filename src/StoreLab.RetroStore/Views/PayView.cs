using StoreLab.ApplicationCore.Models;
using StoreLab.RetroStore.Utils;
using StoreLab.RetroStore.ViewModels;

namespace StoreLab.RetroStore.Views;

public class PayView : ViewBase<PayViewModel, PayViewModelActions>
{
    public override string GetPromptText(PayViewModel viewModel)
    {
        return "(Pay) ";
    }
    public override string GetHeaderText(PayViewModel viewModel)
    {
        return $"State: {viewModel?.State}";
    }

    public override void RenderInternal(PayViewModel viewModel)
    {
        var columns = new List<TableColumn<PaymentItem>>
        {
            new TableColumn<PaymentItem>("#", 0, (item, idx) => (idx + 1).ToString()),
            new TableColumn<PaymentItem>("Type", 16, (item, idx) => item.Type.ToString()),
            new TableColumn<PaymentItem>("Amount", 12, (item, idx) => item.Amount.ToString(), alignRight: true)
        };
        TableColumnHelper.PrintTable(viewModel?.Payments ?? new List<PaymentItem>(), columns);
        string label = "Remaining amount:";
        string value = $"{viewModel?.RemainingAmount ?? 0}";
        PrintBottomSection(label, value);
    }

    public void ShowPaymentDone(PayViewModel viewModel, PaymentType paymentType, int paymentAmount)
    {
        // Add view data for item added
        AddViewData(viewModel, PayViewModelActions.ShowPaymentDone, ViewDataMode.Info, $"Payed {paymentAmount} with {paymentType}");
        Render(viewModel);
    }
}
