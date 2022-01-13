using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Catalogue.Models;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Catalogue.Controllers
{
    [Area("Catalogue")]
    public class BakeryController : Controller
    {
        private readonly IShop shop;
        public BakeryController (IShop shop)
        {
            this.shop = shop;
        }
        public async Task<ActionResult> Index()
        {
            var category = Category.Bakery;
            var products = await shop.TryGetProductsByCategoryAsync(category);
            var productsToView = products.ToProductViewModels();
            return View(productsToView);
        }
    }
}
