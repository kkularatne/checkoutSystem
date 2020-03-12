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

        [Fact]
        public void MultiBuyOfferWithOneProductTest()
        {
            // arrange
            var fixture = new Fixture();
            var sku = fixture.Create<string>();
            var unitPrice = 1.0;
            var product = new Product(sku, unitPrice);
            product.MultiBuyOffer = new MultiBuyOffer(0.5,2);
            var orderLine = new OrderLine(product,5);
            var order = new Order();
            order.AddOrderLine(orderLine);

            // act
            var totalPrice = order.TotalPrice;
            var offerPrice = order.CalculatePriceWithOffers();

            // assert
            Assert.Equal(5.0,totalPrice);
            Assert.Equal(3.0,offerPrice);
        }

        [Fact]
        public void MultiBuyOfferWithMultiProductsTest()
        {
            // arrange
            var fixture = new Fixture();
            var sku = fixture.Create<string>();
            var unitPrice = 1.0;
            var productOne = new Product(sku, unitPrice);
            productOne.MultiBuyOffer = new MultiBuyOffer(0.5, 2);
            var orderLineOne = new OrderLine(productOne, 5);

            var skuTwo = fixture.Create<string>();
            var unitPriceTwo = 2.0;
            var productTwo = new Product(skuTwo, unitPriceTwo);
            var orderLineTwo = new OrderLine(productTwo, 5);

            var orderLineThree = new OrderLine(productOne,5);

            var order = new Order();
            order.AddOrderLine(orderLineOne);
            order.AddOrderLine(orderLineTwo);
            order.AddOrderLine(orderLineThree);

            // act
            var totalPrice = order.TotalPrice;
            var offerPrice = order.CalculatePriceWithOffers();

            // assert
            Assert.Equal(20, totalPrice);
            Assert.Equal(16, offerPrice);
        }
    }
}
