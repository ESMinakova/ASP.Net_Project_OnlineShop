using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UsersController : Controller
    {        
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IdentityContext identityContext;


        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IdentityContext identityContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.identityContext = identityContext;
        }

        public IActionResult Index()
        {
            var users = userManager.Users;
            var usersViewModel = users.Select(x => x.ToUserViewModel()).ToList();
            return View(usersViewModel);
        }

        public async Task<ActionResult> UserDetailsAsync(string login)
        {
            var currentUser = await userManager.FindByEmailAsync(login);
            return View(currentUser.ToUserViewModel());
        }

        public IActionResult ChangePassword(string login)
        {
            var changePassword = new ChangePassword { Login = login };
            return View(changePassword);
        }

        [HttpPost]
        public async Task<ActionResult> ChangePasswordAsync(string login, ChangePassword changePassword)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.FindByEmailAsync(login);
                if (currentUser != null)
                {
                    var newHashPassword = userManager.PasswordHasher.HashPassword(currentUser, changePassword.Password);
                    currentUser.PasswordHash = newHashPassword;
                    await userManager.UpdateAsync(currentUser);
                    return RedirectToAction(nameof(Index));
                }
                else
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
            return View(changePassword);
        }


        public async Task<ActionResult> EditAsync(string login)
        {
            var currentUser = await userManager.FindByEmailAsync(login);
            return View(currentUser.ToUserViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> EditAsync(string login, UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.FindByEmailAsync(login);
                currentUser.UserName = user.Name;
                currentUser.PhoneNumber = user.Phone;
                await identityContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
       

        public async Task<IActionResult> AddRoleAsync(string login)
        {
            var currentUser = await userManager.FindByEmailAsync(login);
            if (currentUser != null)
            {
                var currentUserViewModel = currentUser.ToUserViewModel();
                var currentUserRoles = await userManager.GetRolesAsync(currentUser);
                var allRolesViewModel = roleManager.Roles.Select(x => x.ToRoleViewModel()).ToList();
                var userAndRoles = (currentUserViewModel, currentUserRoles, allRolesViewModel);
                return View(userAndRoles);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> AddRoleAsync(string login, List<string> roles)
        {            
            var currentUser = await userManager.FindByEmailAsync(login);
            if (currentUser != null)
            {
                var userRoles = await userManager.GetRolesAsync(currentUser);                
                var allRoles = roleManager.Roles.ToList();                
                var addedRoles = roles.Except(userRoles);                
                var removedRoles = userRoles.Except(roles);
                await userManager.AddToRolesAsync(currentUser, addedRoles);
                await userManager.RemoveFromRolesAsync(currentUser, removedRoles);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        public async Task<ActionResult> DeleteAsync(string login)
        {
            var user = await userManager.FindByEmailAsync(login);
            if (user != null)
            {
                await userManager.DeleteAsync(user);
                await identityContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
