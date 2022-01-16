using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Catalogue.Models;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Catalogue.Controllers
{
    [Area("Catalogue")]
    public class DrinksController : Controller
    {
        private readonly IShop shop;
        public DrinksController(IShop shop)
        {
            this.shop = shop;
        }
        public async Task<ActionResult> Index()
        {
            var category = Category.Drink;
            var products = await shop.TryGetProductsByCategoryAsync(category);
            var productsToView = products.ToProductViewModels();
            return View(productsToView);
        }
    }
}
