using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CheckoutSystem.Models;
using CheckoutSystem.Services;
using CheckoutSystem.ViewModel;

namespace CheckoutSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly OrderService _orderService = new OrderService();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult myMethod(Item item)
        {
            Order order = _orderService.Scan(item.Sku, item.Quantity);

            ViewBag.TotalPrice = order.TotalPrice;
            ViewBag.PriceAfterOffer = order.CalculatePriceWithOffers();
            return View("Index");
        }
    }
}
