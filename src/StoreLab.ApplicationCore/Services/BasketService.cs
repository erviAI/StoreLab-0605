using StoreLab.ApplicationCore.Interfaces;
using StoreLab.ApplicationCore.Models;

namespace StoreLab.ApplicationCore.Services;

public class BasketService : IBasketService
{
    private IBasketRepository BasketRepository { get; }
    private ICatalogRepository CatalogRepository { get; }

    public BasketService(IBasketRepository basketRepository, ICatalogRepository catalogRepository)
    {
        BasketRepository = basketRepository;
        CatalogRepository = catalogRepository;
    }

    public async Task<Basket> CreateBasket()
    {
        // Get the maximum id from the repository
        var maxId = await BasketRepository.GetMaxBasketId();
        var basket = new Basket()
        {
            Id = maxId + 1
        };
        return await BasketRepository.UpsertBasket(basket);
    }

    public async Task<Basket> AddItemToBasket(int basketId, int catalogId)
    {
        // Get the basket by id
        var basket = await BasketRepository.GetBasketById(basketId);
        if (basket == null)
        {
            throw new Exception("Basket not found");
        }

        // Get the catalog item by id
        var catalogItem = await CatalogRepository.GetCatalogItemById(catalogId);
        if (catalogItem == null)
        {
            throw new Exception("Catalog item not found");
        }

        // Create the BasketItem
        BasketItem basketItem = new BasketItem
        {
            Id = catalogItem.Id,
            Quantity = 1,
            Description = catalogItem.Description,
            Name = catalogItem.Name,
            Price = catalogItem.Price
        };

        // Add the item to the basket
        basket.AddItem(basketItem);

        // Upsert the basket
        return await BasketRepository.UpsertBasket(basket);
    }

    public async Task<Basket> AddPaymentToBasket(int basketId, int amount, PaymentType type)
    {
        // Add payment to the basket
        var basket = await BasketRepository.GetBasketById(basketId);
        if (basket == null)
        {
            throw new Exception("Basket not found");
        }

        // Create the payment item
        var paymentItem = new PaymentItem
        {
            Amount = amount,
            Type = type
        };

        // Add the payment item to the basket
        basket.AddItem(paymentItem);

        // If the basket is now paid, reduce stock for each item
        if (basket.State == BasketState.Paid)
        {
            foreach (var item in basket.Items)
            {
                var catalogItem = await CatalogRepository.GetCatalogItemById(item.Id);
                if (catalogItem != null)
                {
                    catalogItem.RemoveStock(item.Quantity);
                }
            }
        }
        // Upsert the basket
        return await BasketRepository.UpsertBasket(basket);
    }
}
