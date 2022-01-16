using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository cartsRepository;
        private readonly IShop shop;
        private readonly UserManager<User> userManager;

        public CartController(ICartRepository cartsRepository, IShop shop, UserManager<User> userManager)
        {
            this.cartsRepository = cartsRepository;
            this.shop = shop;
            this.userManager = userManager;
        }
        public async Task<ActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var cart = new Cart();
            if (user == null)
            {
                var userId = Request.Cookies["Id"];
                if (userId == null)
                {
                    userId = Guid.NewGuid().ToString() + DateTime.Now.ToShortDateString();
                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Append("id", userId, cookieOptions);
                }                
                cart = await cartsRepository.TryGetCartByUserIdAsync(userId);
            }
            else
                cart = await cartsRepository.TryGetCartByUserIdAsync(user.Id);
            var cartViewModel = cart.ToCartViewModel();
            return View(cartViewModel);
        }

        public async Task<ActionResult> AddAsync(Guid productId)
        {
            var currentProduct = await shop.TryGetProductAsync(productId);
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                var userId = Request.Cookies["Id"];
                if (userId == null)
                {
                    userId = Guid.NewGuid().ToString() + DateTime.Now.ToShortDateString();
                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Append("id", userId, cookieOptions);
                }
                await cartsRepository.AddAsync(currentProduct, userId);
            }
            else
                await cartsRepository.AddAsync(currentProduct, user.Id);
            
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteAsync(Guid productId)
        {
            var currentProduct = await shop.TryGetProductAsync(productId);
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                var userId = Request.Cookies["Id"];
                await cartsRepository.DeleteAsync(currentProduct.Id, userId);
            }
            else
                await cartsRepository .DeleteAsync(currentProduct.Id, user.Id);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> ClearAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                var userId = Request.Cookies["Id"];
                await cartsRepository.ClearAsync(userId);
            }
            else
                await cartsRepository.ClearAsync(user.Id);
            return RedirectToAction("Index");
        }


    }
}
