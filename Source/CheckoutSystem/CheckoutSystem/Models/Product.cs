namespace CheckoutSystem.Models
{
    public class Product
    {
        public Product(string sku, double unitPrice)
        {
            SKU = sku;
            UnitPrice = unitPrice;
        }

        public string SKU { get; set; }
        public double UnitPrice { get; set; }
        public MultiBuyOffer MultiBuyOffer { get; set; }
        public bool IsMultiBuyOfferAvailable => null != MultiBuyOffer;

    }
}