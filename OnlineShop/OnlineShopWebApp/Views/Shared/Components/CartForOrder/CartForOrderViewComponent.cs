using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.FindByEmailAsync(User.Identity.Name);
            var cart = new Cart();
            if (user != null)
                cart = await cartsRepository.TryGetCartByUserIdAsync(user.Id);            
            return View("CartForOrder", cart.ToCartViewModel());
        }
    }
}
