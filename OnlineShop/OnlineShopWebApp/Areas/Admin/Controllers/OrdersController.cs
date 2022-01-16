using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using System.Threading.Tasks;

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
        public async Task<ActionResult> Index()
        {
            var dBOrders = await orderRepository.GetAllAsync();
            var orders = dBOrders.ToOrderWithContactsViewModels();
            return View(orders);
        }

        public async Task<ActionResult> OrderDetailsAsync(Guid orderId)
        {
            var dBOrder = await orderRepository.TryGetOrderByIdAsync(orderId);
            var viewModelOrder = dBOrder.ToOrderWithContactsViewModel();
            return View(viewModelOrder);
        }

        [HttpPost]
        public async Task<ActionResult> SelectStatusAsync(Guid orderId, OrderStatusViewModel status)
        {
            var newStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), status.ToString(), true);
            await orderRepository.SelectStatusAsync(orderId, newStatus);
            return RedirectToAction(nameof(Index));
        }
    }
}
