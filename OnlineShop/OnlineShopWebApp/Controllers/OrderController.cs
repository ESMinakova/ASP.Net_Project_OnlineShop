using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;

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
        public IActionResult SuccessfulOrder(UserDeliveryInfoViewModel userInfo)
        {
            if (ModelState.IsValid)
            {

                var cart = cartsRepository.TryGetCartByUserId(Constants.UserId);
                var order = new OrderWithContactsViewModel { UserDeliveryInfo = userInfo, OrderTime = DateTime.Now, Cart = cart.ToCartViewModel() };
                //Прикрутить добавление адресной информации к User, когда будет функционировать БД юзеров и можно будет избавиться от Constants.UserId и пользоваться нормальными Guid Id                
                var dbOrder = order.ToOrderWithContacts();
                dbOrder.Cart = cartsRepository.Clone(cart);                
                orderRepository.Add(dbOrder);
                cartsRepository.Clear(Constants.UserId);
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
