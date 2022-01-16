using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ActionResult> DeleteAsync(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role != null)            
                await roleManager.DeleteAsync(role);      
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add()
        {
            return View();
        }


        [AcceptVerbs("Get", "Post")]
        public async Task<ActionResult> CheckNameAsync(string name)
        {
            if (await roleManager.FindByNameAsync(name) != null)
                return Json(false);
            return Json(true);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(RoleViewModel role)
        {
            if (ModelState.IsValid)            
                await roleManager.CreateAsync(new IdentityRole(role.Name));               
                                       
            return RedirectToAction(nameof(Index));
        }
    }
}
