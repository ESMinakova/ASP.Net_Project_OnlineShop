using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using System;
using System.Threading.Tasks;

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

        public async Task<ActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
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
                favourite = await favouritesRepository.TryGetFavoriteProductsListByUserIdAsync(userId);
            }
            else
                favourite = await favouritesRepository.TryGetFavoriteProductsListByUserIdAsync(user.Id);
            var favouriteViewModel = favourite.ToFavouriteViewModel();
            return View(favouriteViewModel);
        }

        public async Task<ActionResult> DeleteAsync(Guid productId)
        {
            var currentProduct = await shop.TryGetProductAsync(productId);
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                var userId = Request.Cookies["Id"];
                await favouritesRepository .DeleteAsync(currentProduct.Id, userId);
            }
            else
                await favouritesRepository .DeleteAsync(currentProduct.Id, user.Id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddOrDeleteAsync(Guid productId)
        {
            var currentProduct = await shop.TryGetProductAsync (productId);
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
                await favouritesRepository.AddOrDeleteAsync(currentProduct, userId);
            }
            else
                await favouritesRepository.AddOrDeleteAsync(currentProduct, user.Id);
            return RedirectToAction("Index");
        }
    }
}
