using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Views.Shared.Components.AdminsMenu
{
    public class AdminsMenuViewComponent : ViewComponent
    {


        public IViewComponentResult Invoke()
        {
            return View("AdminsMenu");
        }
    }
}
