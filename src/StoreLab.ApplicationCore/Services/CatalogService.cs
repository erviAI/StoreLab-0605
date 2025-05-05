using StoreLab.ApplicationCore.Interfaces;
using StoreLab.ApplicationCore.Models;

namespace StoreLab.ApplicationCore.Services;

public class CatalogService : ICatalogService
{
    private ICatalogRepository CatalogRepository { get; }

    public CatalogService(ICatalogRepository catalogRepository)
    {
        CatalogRepository = catalogRepository;
    }

    public Task<List<CatalogItem>> Search(string searchInput)
    {
        return CatalogRepository.Search(searchInput);
    }

    public Task<CatalogItem?> GetCatalogItemById(int id)
    {
        return CatalogRepository.GetCatalogItemById(id);
    }
}
