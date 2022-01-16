using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Views.Shared.Components.CartIcon
{
    public class CartIconViewComponent : ViewComponent
    {
        private readonly ICartRepository cartRepository;
        private readonly UserManager<User> userManager;

        public CartIconViewComponent(ICartRepository cartRepository, UserManager<User> userManager)
        {
            this.cartRepository = cartRepository;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var cart = new Cart();
            if (user == null)
            {
                var userId = Request.Cookies["Id"];
                cart = cartRepository.TryGetCartByUserId(userId);
            }
            else               
                cart = cartRepository.TryGetCartByUserId(user.Id);
            var productCount = cart.ToCartViewModel()?.Amount ?? 0;
            return View("CartIcon", productCount);
        }
    }
}
