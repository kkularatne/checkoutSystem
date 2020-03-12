using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckoutSystem.Models;

namespace CheckoutSystem.Repository
{
    public class OrderRepository
    {
        private readonly List<Product> _products = new List<Product>();
        public OrderRepository()
        {
           SetUpProductList();
           SetUpMultiBuyOffers();
        }

        private void SetUpProductList()
        {
            _products.Add(new Product("A99", 0.50));
            _products.Add(new Product("B15", 0.30));
            _products.Add(new Product("C40", 0.60));
        }

        private void SetUpMultiBuyOffers()
        {
            _products.First(p => p.SKU == "A99").MultiBuyOffer = new MultiBuyOffer(0.433,3); // 3-1.30
            _products.First(p => p.SKU == "B15").MultiBuyOffer = new MultiBuyOffer(0.225,2);// 2 - 0.45
        }

        public Product GetBySku(string sku)
        {
           return _products.FirstOrDefault(p => p.SKU == sku);
        }
    }
}
