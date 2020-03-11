namespace CheckoutSystem.Models
{
    public class OrderLine
    {
        public OrderLine(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;

        }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}