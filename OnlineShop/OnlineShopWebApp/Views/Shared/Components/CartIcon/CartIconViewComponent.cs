using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var cart = new Cart();
            if (user == null)
            {
                var userId = Request.Cookies["Id"];
                cart = await cartRepository.TryGetCartByUserIdAsync(userId);
            }
            else               
                cart = await cartRepository.TryGetCartByUserIdAsync(user.Id);
            var productCount = cart.ToCartViewModel()?.Amount ?? 0;
            return View("CartIcon", productCount);
        }
    }
}
