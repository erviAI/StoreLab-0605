using StoreLab.ApplicationCore.Models;

namespace StoreLab.ApplicationCore.Interfaces;

public interface ICatalogRepository
{
    // Get the catalog item by id
    Task<CatalogItem?> GetCatalogItemById(int id);
    // Search for catalog items
    Task<List<CatalogItem>> Search(string searchInput);
}
