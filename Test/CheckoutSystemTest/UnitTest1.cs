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
            var unitPrice = 0.5;
            var productOne = new Product(sku, unitPrice);
            productOne.MultiBuyOffer = new MultiBuyOffer(0.433, 3);

            var skuTwo = fixture.Create<string>();
            var unitPriceTwo = 0.30;
            var productTwo = new Product(skuTwo, unitPriceTwo);
            productTwo.MultiBuyOffer = new MultiBuyOffer(0.225, 2);

            // act
            var order = new Order();
            order.AddOrderLine(new OrderLine(productOne, 1));
            var totalPrice = order.TotalPrice;
            var offerPrice = order.CalculatePriceWithOffers();

            // assert
            Assert.Equal(0.5, order.TotalPrice);
            Assert.Equal(0.5, order.CalculatePriceWithOffers());

            // act
            order.AddOrderLine(new OrderLine(productOne, 1));

            // assert
            Assert.Equal(1, order.TotalPrice);
            Assert.Equal(1, order.CalculatePriceWithOffers());

            // act
            order.AddOrderLine(new OrderLine(productTwo, 1));

            // assert
            Assert.Equal(1.30, order.TotalPrice);
            Assert.Equal(1.30, order.CalculatePriceWithOffers());

            // act
            order.AddOrderLine(new OrderLine(productOne, 1));

            // assert - multi buy offer must be applied.
            Assert.Equal(1.80, order.TotalPrice);
            Assert.Equal(1.60, order.CalculatePriceWithOffers());

            // act
            order.AddOrderLine(new OrderLine(productTwo, 1));

            // assert - multi buy offer must be applied.
            Assert.Equal(2.10, order.TotalPrice);
            Assert.Equal(1.75, order.CalculatePriceWithOffers());

            // act
            order.AddOrderLine(new OrderLine(productOne, 1));

            // assert
            Assert.Equal(2.60, order.TotalPrice);
            Assert.Equal(2.25, order.CalculatePriceWithOffers());

        }
    }
}
