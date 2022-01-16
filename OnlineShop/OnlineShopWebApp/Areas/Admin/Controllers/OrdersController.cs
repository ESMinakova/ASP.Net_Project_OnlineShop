using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;


namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
            var dBOrders = orderRepository.GetAll();
            var orders = dBOrders.ToOrderWithContactsViewModels();
            return View(orders);
        }

        public IActionResult OrderDetails(Guid orderId)
        {
            var dBOrder = orderRepository.TryGetOrderById(orderId);
            var viewModelOrder = dBOrder.ToOrderWithContactsViewModel();
            return View(viewModelOrder);
        }

        [HttpPost]
        public IActionResult SelectStatus(Guid orderId, OrderStatusViewModel status)
        {
            var newStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), status.ToString(), true);
            orderRepository.SelectStatus(orderId, newStatus);
            return RedirectToAction(nameof(Index));
        }
    }
}
