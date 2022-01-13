using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.Linq;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IdentityContext identityContext;

        public RolesController(RoleManager<IdentityRole> roleManager, IdentityContext identityContext)
        {
            this.roleManager = roleManager;
            this.identityContext = identityContext;
        }

        public IActionResult Index()
        {
            var rolesViewModel = roleManager.Roles.Select(x => x.ToRoleViewModel()).ToList();
            return View(rolesViewModel);
        }

        public IActionResult Delete(string roleName)
        {
            var role = roleManager.FindByNameAsync(roleName).Result;
            if (role != null)            
                roleManager.DeleteAsync(role).Wait();      
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add()
        {
            return View();
        }


        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckName(string name)
        {
            if (roleManager.FindByNameAsync(name).Result != null)
                return Json(false);
            return Json(true);
        }

        [HttpPost]
        public IActionResult Add(RoleViewModel role)
        {
            if (ModelState.IsValid)            
                roleManager.CreateAsync(new IdentityRole(role.Name)).Wait();               
                                       
            return RedirectToAction(nameof(Index));
        }
    }
}
