using StoreLab.ApplicationCore.Models;
using StoreLab.RetroStore.Enums;

namespace StoreLab.RetroStore.ViewModels;

public class SellViewModel : BaseViewModel<SellViewModelActions>
{
    public List<BasketItem> Items { get; set; } = new();
    public int TotalAmount { get; set; }

    public SellViewModel(Basket? basket, CommonActions actions)
    {
        State = basket?.State.ToString() ?? string.Empty;
        Items = basket?.Items ?? new List<BasketItem>();
        TotalAmount = basket?.TotalAmount ?? 0;
        Actions = actions;
    }
}

public enum SellViewModelActions
{
    ShowItemNotFound, 
    ShowItemAdded
}