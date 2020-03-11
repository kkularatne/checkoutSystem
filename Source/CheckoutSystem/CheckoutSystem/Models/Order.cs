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

        public double CalculatePriceWithOffers()
        {
            double offerPrice = 0.0;
            foreach (var orderLine in OrderLines)
            {
                if (IsQualifyForMultiBuyOffer(orderLine))
                {
                    offerPrice += CalculateUnderOfferPrice(orderLine);
                    offerPrice += CalculateOverOfferPrice(orderLine);
                }
                else
                {
                    offerPrice += orderLine.Product.UnitPrice * orderLine.Quantity;
                }
            }

            return offerPrice;
        }

        private static bool IsQualifyForMultiBuyOffer(OrderLine orderLine)
        {
            return orderLine.Product.IsMultiBuyOfferAvailable &&
                   orderLine.Quantity >= orderLine.Product.MultiBuyOffer.Threshold;
        }

        private static double CalculateUnderOfferPrice(OrderLine orderLine)
        {
            var quantity = orderLine.Quantity / orderLine.Product.MultiBuyOffer.Threshold;
            return orderLine.Product.MultiBuyOffer.Threshold * orderLine.Product.MultiBuyOffer.UnitPrice * quantity;
        }

        private static double CalculateOverOfferPrice(OrderLine orderLine)
        {
            var quantity = orderLine.Quantity % orderLine.Product.MultiBuyOffer.Threshold;
            return orderLine.Product.UnitPrice * quantity;
        }
    }
}