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

        public IActionResult UserDetails(string login)
        {
            var currentUser = userManager.FindByEmailAsync(login).Result;
            return View(currentUser.ToUserViewModel());
        }

        public IActionResult ChangePassword(string login)
        {
            var changePassword = new ChangePassword { Login = login };
            return View(changePassword);
        }

        [HttpPost]
        public IActionResult ChangePassword(string login, ChangePassword changePassword)
        {
            if (ModelState.IsValid)
            {
                var currentUser = userManager.FindByEmailAsync(login).Result;
                if (currentUser != null)
                {
                    var newHashPassword = userManager.PasswordHasher.HashPassword(currentUser, changePassword.Password);
                    currentUser.PasswordHash = newHashPassword;
                    userManager.UpdateAsync(currentUser).Wait();
                    return RedirectToAction(nameof(Index));
                }
                else
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
            return View(changePassword);
        }


        public IActionResult Edit(string login)
        {
            var currentUser = userManager.FindByEmailAsync(login).Result;
            return View(currentUser.ToUserViewModel());
        }

        [HttpPost]
        public IActionResult Edit(string login, UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var currentUser = userManager.FindByEmailAsync(login).Result;
                currentUser.UserName = user.Name;
                currentUser.PhoneNumber = user.Phone;
                identityContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
       

        public IActionResult AddRole(string login)
        {
            var currentUser = userManager.FindByEmailAsync(login).Result;
            if (currentUser != null)
            {
                var currentUserViewModel = currentUser.ToUserViewModel();
                var currentUserRoles = userManager.GetRolesAsync(currentUser).Result.ToList();
                var allRolesViewModel = roleManager.Roles.Select(x => x.ToRoleViewModel()).ToList();
                var userAndRoles = (currentUserViewModel, currentUserRoles, allRolesViewModel);
                return View(userAndRoles);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddRole(string login, List<string> roles)
        {            
            var currentUser = userManager.FindByEmailAsync(login).Result;
            if (currentUser != null)
            {
                var userRoles = userManager.GetRolesAsync(currentUser).Result;                
                var allRoles = roleManager.Roles.ToList();                
                var addedRoles = roles.Except(userRoles);                
                var removedRoles = userRoles.Except(roles);
                userManager.AddToRolesAsync(currentUser, addedRoles).Wait();
                userManager.RemoveFromRolesAsync(currentUser, removedRoles).Wait();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        public IActionResult Delete(string login)
        {
            var user = userManager.FindByEmailAsync(login).Result;
            if (user != null)
            {
                userManager.DeleteAsync(user).Wait();
                identityContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
