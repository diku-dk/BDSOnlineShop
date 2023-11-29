namespace ECommerce.Olep.Schema
{
    [Serializable]
    public sealed class Inventory
    {
        public readonly long customerId;
        public readonly double price;
        public readonly int quantity;

        public Inventory(long customerId, double price, int quantity)
        {
            this.customerId = customerId;
            this.price = price;
            this.quantity = quantity;
        }
    }
}

