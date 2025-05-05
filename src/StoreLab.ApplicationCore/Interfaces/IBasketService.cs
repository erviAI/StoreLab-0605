using StoreLab.ApplicationCore.Models;

namespace StoreLab.ApplicationCore.Interfaces;

public interface IBasketService
{
    // Create a new basket
    Task<Basket> CreateBasket();

    // Add an item to the basket by catalogId
    Task<Basket> AddItemToBasket(int basketId, int catalogId);

    // Add payment method
    Task<Basket> AddPaymentToBasket(int basketId, int amount, PaymentType type);
}
