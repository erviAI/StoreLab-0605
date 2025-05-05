using StoreLab.ApplicationCore.Models;

namespace StoreLab.ApplicationCore.Tests;

public class BasketTests
{
    [Test]
    public void AddItem_ShouldAddNewItemToBasket()
    {
        var basket = new Basket();
        var item = new BasketItem
        {
            Id = 1,
            Price = 10,
            Quantity = 2,
            Name = "Sample Item",
            Description = "Sample Description"
        };

        basket.AddItem(item);

        Assert.That(basket.Items, Has.Exactly(1).Items);
        Assert.That(basket.Items[0].Id, Is.EqualTo(1));
        Assert.That(basket.Items[0].Quantity, Is.EqualTo(2));
    }

    [Test]
    public void AddItem_ShouldIncreaseQuantityIfItemExists()
    {
        var basket = new Basket();
        var item1 = new BasketItem {Id = 1, Price = 10, Quantity = 2, Name = "Sample Item", Description = "Sample Description" };
        var item2 = new BasketItem {Id = 1, Price = 10, Quantity = 3, Name = "Sample Item", Description = "Sample Description" };

        basket.AddItem(item1);
        basket.AddItem(item2);

        Assert.That(basket.Items, Has.Exactly(1).Items);
        Assert.That(basket.Items[0].Quantity, Is.EqualTo(5));
    }
}