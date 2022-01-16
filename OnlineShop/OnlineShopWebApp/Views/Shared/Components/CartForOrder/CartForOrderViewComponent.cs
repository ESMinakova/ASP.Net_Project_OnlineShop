using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Views.Shared.Components.CartForOrder
{
    public class CartForOrderViewComponent : ViewComponent
    {
        private readonly ICartRepository cartsRepository;
        private readonly UserManager<User> userManager;

        public CartForOrderViewComponent(ICartRepository cartsRepository, UserManager<User> userManager)
        {
            this.cartsRepository = cartsRepository;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = userManager.FindByEmailAsync(User.Identity.Name).Result;
            var cart = new Cart();
            if (user != null)
                cart = cartsRepository.TryGetCartByUserId(user.Id);            
            return View("CartForOrder", cart.ToCartViewModel());
        }
    }
}
