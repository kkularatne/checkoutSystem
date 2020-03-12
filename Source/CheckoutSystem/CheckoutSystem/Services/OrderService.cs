using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckoutSystem.Models;
using CheckoutSystem.Repository;

namespace CheckoutSystem.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository = new OrderRepository();
        private static Order _order;

        private static Order Order
        {
            get {
                if (_order == null)
                {
                    _order = new Order();
                }

                return _order;
            }
        }

        public Order Scan(string sku, int quantity)
        {
            var product = _orderRepository.GetBySku(sku);
            if (null == product)
            {
                throw new Exception("Product does not exists in the system");
            }

            var orderLine = new OrderLine(product, quantity);
            Order.AddOrderLine(orderLine);
            return Order;
        }
    }
}
