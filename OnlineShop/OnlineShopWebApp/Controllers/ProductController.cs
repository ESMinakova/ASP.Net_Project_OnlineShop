using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using System;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IShop shop;
        public ProductController(IShop shop)
        {
            this.shop = shop;
        }

        public IActionResult Index(Guid productId)
        {                    
            var currentProduct = shop.TryGetProduct(productId);             
            return View(currentProduct.ToProductViewModel());            
        }

        [HttpPost]
        public IActionResult Search(string request)
        {
            var results = shop.TryGetProducts(request);
            return View(results.ToProductViewModels());
        }
    }
}
