using StoreLab.ApplicationCore.Interfaces;
using StoreLab.ApplicationCore.Models;

namespace StoreLab.ApplicationCore.Infrastructure;

public class InMemoryCatalogRepository : ICatalogRepository
{
    private readonly List<CatalogItem> _catalogItems;

    public InMemoryCatalogRepository(Func<List<CatalogItem>> catalogItemsFactory)
    {
        _catalogItems = catalogItemsFactory.Invoke();
    }

    public Task<List<CatalogItem>> GetAllCatalogItems()
    {
        // Return the list of catalog items as a task
        return Task.FromResult(_catalogItems);
    }

    public Task<CatalogItem?> GetCatalogItemById(int id)
    {
        return Task.FromResult(_catalogItems.FirstOrDefault(item => item.Id == id));
    }

    public Task<List<CatalogItem>> Search(string searchInput)
    {
        var filteredItems = _catalogItems.AsEnumerable();

        // Return ALL if star
        if (searchInput != "*")
            filteredItems = filteredItems
            .Where(item => item.Name.Contains(searchInput, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(filteredItems.ToList());
    }
}
