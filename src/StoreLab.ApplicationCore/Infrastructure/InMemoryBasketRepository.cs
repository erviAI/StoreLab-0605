using StoreLab.ApplicationCore.Interfaces;
using StoreLab.ApplicationCore.Models;

namespace StoreLab.ApplicationCore.Infrastructure;

public class InMemoryBasketRepository : IBasketRepository
{
    private readonly Dictionary<int, Basket> _baskets = new();
    public Task<int> GetMaxBasketId()
    {
        // Get the maximum id from the dictionary
        if (_baskets.Count == 0)
        {
            return Task.FromResult(0);
        }
        var maxId = _baskets.Keys.Max();
        return Task.FromResult(maxId);
    }

    public Task<Basket?> GetBasketById(int id)
    {
        // Get the basket by id
        if (_baskets.TryGetValue(id, out var basket))
        {
            return Task.FromResult(basket);
        }

        return Task.FromResult<Basket?>(null);
    }

    public Task<Basket> UpsertBasket(Basket basket)
    {
        if (basket.Id == 0)
        {
            // Guard against 0 id
            throw new ArgumentException("Basket Id cannot be 0");
        }

        // Upsert the basket
        if (_baskets.ContainsKey(basket.Id))
        {
            _baskets[basket.Id] = basket;
        }
        else
        {
            _baskets.Add(basket.Id, basket);
        }
        return Task.FromResult(basket);
    }
}
