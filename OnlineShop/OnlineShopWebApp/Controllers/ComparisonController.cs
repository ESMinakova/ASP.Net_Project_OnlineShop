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
    public class ComparisonController : Controller
    {
        private readonly IComparisonRepository comparisonRepository;
        private readonly IShop shop;
        private readonly UserManager<User> userManager;

        public ComparisonController(IComparisonRepository comparisonRepository, IShop shop, UserManager<User> userManager)
        {
            this.comparisonRepository = comparisonRepository;
            this.shop = shop;
            this.userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var comparison = new Comparison();
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
                comparison = await comparisonRepository.TryGetComparisonByUserIdAsync(userId);
            }
            else
                comparison = await comparisonRepository .TryGetComparisonByUserIdAsync(user.Id);
            var comparisonViewModel = comparison.ToComparisonViewModel();
            return View(comparisonViewModel);
        }

        public async Task<ActionResult> AddAsync(Guid productId)
        {
            var currentProduct = await shop.TryGetProductAsync(productId);
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
                await comparisonRepository .AddAsync(currentProduct, userId);
            }
            else
                await comparisonRepository .AddAsync(currentProduct, user.Id);
            return RedirectToAction("Index");


        }
        public async Task<ActionResult> DeleteAsync(Guid productId)
        {
            var currentProduct = await shop.TryGetProductAsync(productId);
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                var userId = Request.Cookies["Id"];
                await comparisonRepository .DeleteAsync(currentProduct.Id, userId);
            }
            else
                await comparisonRepository .DeleteAsync(currentProduct.Id, user.Id);
            return RedirectToAction("Index");
        }
    }
}
