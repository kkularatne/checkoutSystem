using System;
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
            return Math.Round(OrderLines.Sum(orderLine => orderLine.Product.UnitPrice * orderLine.Quantity),2);
        }

        public double CalculatePriceWithOffers()
        {
            var offerPrice = 0.0;
            var groupedBySku = GroupedBySku();

            foreach (var orderLine in groupedBySku)
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

            return Math.Round(offerPrice, 2);
        }

        private List<OrderLine> GroupedBySku()
        {
            return OrderLines.GroupBy(order => order.Product.SKU).Select(s => new OrderLine
                (s.First().Product,s.Sum(x => x.Quantity))).ToList();
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