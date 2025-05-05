namespace StoreLab.ApplicationCore.Models;

public class CatalogItem
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    // Price of the item
    public int Price { get; set; }

    // Quantity in stock
    public int AvailableStock { get; set; }

    // Implement methods to add, remove
    // and update the quantity in stock
    public void AddStock(int quantity)
    {
        AvailableStock += quantity;
    }

    public void RemoveStock(int quantity)
    {
        AvailableStock -= quantity;
    }
}
