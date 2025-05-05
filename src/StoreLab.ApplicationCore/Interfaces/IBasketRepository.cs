using StoreLab.ApplicationCore.Models;

namespace StoreLab.ApplicationCore.Interfaces;

public interface IBasketRepository
{
    // Get max basket id
    Task<int> GetMaxBasketId();

    // Get the basket by id
    Task<Basket?> GetBasketById(int id);

    // Upsert the basket
    Task<Basket> UpsertBasket(Basket basket);
}
