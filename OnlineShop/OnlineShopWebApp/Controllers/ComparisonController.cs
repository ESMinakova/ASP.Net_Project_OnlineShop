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
        public IActionResult Index()
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
                comparison = comparisonRepository.TryGetComparisonByUserId(userId);
            }
            else
                comparison = comparisonRepository.TryGetComparisonByUserId(user.Id);
            var comparisonViewModel = comparison.ToComparisonViewModel();
            return View(comparisonViewModel);
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
                comparisonRepository.Add(currentProduct, userId);
            }
            else
                comparisonRepository.Add(currentProduct, user.Id);
            return RedirectToAction("Index");


        }
        public IActionResult Delete(Guid productId)
        {
            var currentProduct = shop.TryGetProduct(productId);
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                var userId = Request.Cookies["Id"];
                comparisonRepository.Delete(currentProduct.Id, userId);
            }
            else
                comparisonRepository.Delete(currentProduct.Id, user.Id);
            return RedirectToAction("Index");
        }
    }
}
