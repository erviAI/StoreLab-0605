using StoreLab.ApplicationCore.Models;
using StoreLab.RetroStore.Enums;

namespace StoreLab.RetroStore.ViewModels;

public class CatalogSearchViewModel : BaseViewModel<CatalogSearchViewModelActions>
{
    public List<CatalogItem> Items { get; set; } = new ();
    public int SearchCount => Items?.Count ?? 0;
    
    // Mode enum: select, search - enum type
    public CatalogSearchMode Mode { get; set; } = CatalogSearchMode.Select;
    public Basket? Basket { get; set; }

    public CatalogSearchViewModel() { }

    public CatalogSearchViewModel(Basket? basket, CommonActions actions, List<CatalogItem> items, CatalogSearchMode mode)
    {
        Basket = basket;
        Actions = actions;
        Items = items;
        Mode = mode;
    }

    public enum CatalogSearchMode
    {
        Search,
        Select,
    }
}

public enum CatalogSearchViewModelActions
{
    NoResult
}
