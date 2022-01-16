using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using System;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IShop shop;
        public ProductController(IShop shop)
        {
            this.shop = shop;
        }

        public async Task<ActionResult> Index(Guid productId)
        {                    
            var currentProduct = await shop.TryGetProductAsync(productId);             
            return View(currentProduct.ToProductViewModel());            
        }

        [HttpPost]
        public async Task<ActionResult> SearchAsync(string request)
        {
            var results = await shop.TryGetProductsAsync(request);
            return View(results.ToProductViewModels());
        }
    }
}
