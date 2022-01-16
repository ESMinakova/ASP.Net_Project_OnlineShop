using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using System;

namespace OnlineShopWebApp.Controllers
{
    
    public class FavouriteController : Controller
    {
        private readonly IFavouritesRepository favouritesRepository;
        private readonly IShop shop;
        private readonly UserManager<User> userManager;


        public FavouriteController(IFavouritesRepository favouritesRepository, IShop shop, UserManager<User> userManager)
        {
            this.favouritesRepository = favouritesRepository;
            this.shop = shop;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var favourite = new Favourite();
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
                favourite = favouritesRepository.TryGetFavoriteProductsListByUserId(userId);
            }
            else
                favourite = favouritesRepository.TryGetFavoriteProductsListByUserId(user.Id);
            var favouriteViewModel = favourite.ToFavouriteViewModel();
            return View(favouriteViewModel);
        }

        public IActionResult Delete(Guid productId)
        {
            var currentProduct = shop.TryGetProduct(productId);
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                var userId = Request.Cookies["Id"];
                favouritesRepository.Delete(currentProduct.Id, userId);
            }
            else
                favouritesRepository.Delete(currentProduct.Id, user.Id);
            return RedirectToAction("Index");
        }

        public IActionResult AddOrDelete(Guid productId)
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
                favouritesRepository.AddOrDelete(currentProduct, userId);
            }
            else
                favouritesRepository.AddOrDelete(currentProduct, user.Id);
            return RedirectToAction("Index");
        }
    }
}
