using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICartRepository cartsRepository;
        private readonly IOrderRepository orderRepository; 

        public OrderController(ICartRepository cartsRepository, IOrderRepository orderRepository)
        {
            this.cartsRepository = cartsRepository;
            this.orderRepository = orderRepository;
        }

        public IActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SuccessfulOrderAsync(UserDeliveryInfoViewModel userInfo)
        {
            if (ModelState.IsValid)
            {

                var cart = await cartsRepository.TryGetCartByUserIdAsync(Constants.UserId);
                var order = new OrderWithContactsViewModel { UserDeliveryInfo = userInfo, OrderTime = DateTime.Now, Cart = cart.ToCartViewModel() };                
                var dbOrder = order.ToOrderWithContacts();
                dbOrder.Cart = cartsRepository.Clone(cart);                
                await orderRepository.AddAsync(dbOrder);
                await cartsRepository.ClearAsync(Constants.UserId);
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
