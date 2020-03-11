using System;
using AutoFixture;
using CheckoutSystem.Models;
using Xunit;

namespace CheckoutSystemTest
{
    public class UnitTest1
    {
        [Fact]
        public void OrderTest()
        {
            // arrange
            var fixture = new Fixture();
            var sku = fixture.Create<string>();
            var unitPrice = fixture.Create<double>();
            var quantity = fixture.Create<int>();
            var product = new Product(sku, unitPrice);
            var orderLine1 = new OrderLine(product, quantity);
            var order = new Order();
            order.AddOrderLine(orderLine1);

            // act
            var result = order.TotalPrice;

            // assert
            Assert.Equal(unitPrice * quantity, result);
        }
    }
}
