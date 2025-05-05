namespace StoreLab.ApplicationCore.Models
{
    public class Basket
    {
        public int Id { get; set; }

        public List<BasketItem> Items { get; set; } = [];
        public List<PaymentItem> Payments { get; set; } = [];

        // Read only property of total amount of the basket
        public int TotalAmount
        {
            get
            {
                // Calculate the total amount of the basket
                return Items.Sum(item => item.Price * item.Quantity);
            }
        }

        // Read only property of remaining amount to pay for the basket
        public int RemainingAmount
        {
            get
            {
                // Calculate the remaining amount of the basket
                return TotalAmount - Payments.Sum(payment => payment.Amount);
            }
        }

        public BasketState State { get; set; } = BasketState.Open;

        // Add a new item to the basket
        public void AddItem(BasketItem item)
        {
            // Check if the item already exists in the basket
            var existingItem = Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                // If it does, increase the quantity
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                // If it doesn't, add it to the basket
                Items.Add(item);
            }
        }

        // Add a new payment item to the basket
        public void AddItem(PaymentItem item)
        {
            // If it doesn't, add it to the basket
            Payments.Add(item);

            // Set state to paid if the remaining amount is < 0
            if (RemainingAmount <= 0)
            {
                VerifyBasketItems();
                VerifyPayments();
                State = BasketState.Paid;
            }
        }

        private void VerifyBasketItems()
        {
            // Check that the the quanity of the basket items is not equal to a prime number > 3
            var totalQuantity = Items.Sum(x => x.Quantity);
            if (totalQuantity > 3 && totalQuantity.IsPrime())
            {
                // Not allowed to have a prime number of items in the basket
                throw new InvalidOperationException("Not allowed to have a prime number of items in the basket, when the total quantity is > 3");
            }
        }

        private void VerifyPayments()
        {
            // Check that the count payment could with cash is not equal to 4
            if (Payments.Count(x => x.Type == PaymentType.Cash) == 4)
            {
                throw new InvalidOperationException("Total payment could with cash is not equal to 4");
            }
        }
    }
}
