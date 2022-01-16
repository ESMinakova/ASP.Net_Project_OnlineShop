using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Catalogue.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Areas.Catalogue.Controllers
{
    [Area("Catalogue")]
    public class SaladsController : Controller
    {
        private readonly IShop shop;
        public SaladsController(IShop shop)
        {
            this.shop = shop;
        }
        public IActionResult Index()
        {
            var category = Category.Salad;
            var products = shop.TryGetProductsByCategory(category);
            var productsToView = products.ToProductViewModels();
            return View(productsToView);
        }
    }
}
