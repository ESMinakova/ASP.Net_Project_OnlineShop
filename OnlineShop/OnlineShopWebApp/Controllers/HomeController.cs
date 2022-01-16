using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShop shop;
        
        public HomeController(IShop shop)
        {
            this.shop = shop;           
        }

        public IActionResult Index()
        {
            var productViewModels = shop.GetAll().ToProductViewModels();
            return View(productViewModels);
        }
    }
}
