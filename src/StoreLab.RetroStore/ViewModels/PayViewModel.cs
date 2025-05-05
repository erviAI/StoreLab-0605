using StoreLab.ApplicationCore.Models;
using StoreLab.RetroStore.Enums;

namespace StoreLab.RetroStore.ViewModels;

public class PayViewModel : BaseViewModel<PayViewModelActions>
{
    public List<PaymentItem> Payments { get; set; } = new();
    public int RemainingAmount { get; set; }

    public PayViewModel(Basket? basket, CommonActions actions)
    {
        State = basket?.State.ToString() ?? string.Empty;
        Payments = basket?.Payments ?? new List<PaymentItem>();
        RemainingAmount = basket?.RemainingAmount ?? 0;
        Actions = actions;
    }
}

public enum PayViewModelActions
{
    ShowPaymentDone,
    ShowNoBasket,
    ShowInvalidInput,
    ShowError
}