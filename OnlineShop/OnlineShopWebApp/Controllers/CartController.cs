using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using System;
using System.Linq;

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
        public IActionResult Index()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
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
                cart = cartsRepository.TryGetCartByUserId(userId);
            }
            else
                cart = cartsRepository.TryGetCartByUserId(user.Id);
            var cartViewModel = cart.ToCartViewModel();
            return View(cartViewModel);
        }

        public IActionResult Add(Guid productId)
        {
            var currentProduct = shop.TryGetProduct(productId);
            var user = userManager.GetUserAsync(HttpContext.User).Result;
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
                cartsRepository.Add(currentProduct, userId);
            }
            else
                cartsRepository.Add(currentProduct, user.Id);
            
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid productId)
        {
            var currentProduct = shop.TryGetProduct(productId);
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                var userId = Request.Cookies["Id"];
                cartsRepository.Delete(currentProduct.Id, userId);
            }
            else
                cartsRepository.Delete(currentProduct.Id, user.Id);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                var userId = Request.Cookies["Id"];
                cartsRepository.Clear(userId);
            }
            else
                cartsRepository.Clear(user.Id);
            return RedirectToAction("Index");
        }


    }
}
