using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShop shop;
        
        public HomeController(IShop shop)
        {
            this.shop = shop;           
        }

        public async Task<ActionResult> Index()
        {
            var products = await shop.GetAllAsync();
            
            return View(products.ToProductViewModels());
        }
    }
}
