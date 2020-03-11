using System.Collections.Generic;
using System.Linq;

namespace CheckoutSystem.Models
{
    public class Order
    {
        public Order()
        {
            OrderLines = new List<OrderLine>();
        }

        private List<OrderLine> OrderLines { get; set; }

        public double TotalPrice => CalculateTotalPrice();

        public void AddOrderLine(OrderLine orderLine)
        {
            OrderLines.Add(orderLine);
        }

        private double CalculateTotalPrice()
        {
            return OrderLines.Sum(orderLine => orderLine.Product.UnitPrice * orderLine.Quantity);
        }
    }
}