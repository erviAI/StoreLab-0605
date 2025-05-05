using StoreLab.ApplicationCore.Models;

namespace StoreLab.ApplicationCore.Interfaces;

public interface ICatalogService
{
    Task<List<CatalogItem>> Search(string searchInput);

    Task<CatalogItem?> GetCatalogItemById(int id);  
}
