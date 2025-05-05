namespace StoreLab.ApplicationCore.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        // Price of the item
        public int Price { get; set; }
        // Quantity in basket
        public int Quantity { get; set; }
    }
}