using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using Serilog;
using System;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {        
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<Startup> logger;
        private readonly ICartRepository cartRepository;
        private readonly IComparisonRepository comparisonRepository;
        private readonly IFavouritesRepository favouritesRepository;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<Startup> logger, 
            ICartRepository cartRepository, IComparisonRepository comparisonRepository, IFavouritesRepository favouritesRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.cartRepository = cartRepository;
            this.comparisonRepository = comparisonRepository;
            this.favouritesRepository = favouritesRepository;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new UserSignInInfo { ReturnUrl = returnUrl });
        }

        public IActionResult Register(string returnUrl)
        {
            return View(new UserRegisterInfo { ReturnUrl = returnUrl });
        }

        public async Task<ActionResult> LogoutAsync()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<ActionResult> CheckNonExistentLoginAsync(string login)
        {
            if (await IsUserExistsAsync(login))
                return Json(false);
            return Json(true);
        }


        [AcceptVerbs("Get", "Post")]
        public async Task<ActionResult> CheckExistentLoginAsync(string login)
        {
            if (!await IsUserExistsAsync(login))
                return Json(false);
            return Json(true);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAsync(UserSignInInfo userInfo)
        {            
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(userInfo.Login, userInfo.Password, userInfo.IsMemorize, false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(userInfo.Login);
                    if (user != null)
                    {
                        // получение данных у неавторизованного пользователя 
                        var unauthorizedUserData = await TryGetDataForAuthorizedUserAsync(); 
                        // перенос корзины, списка сравнения и избранного от неавторизованного пользователя
                        await MoveDataToAuthorizedUserAsync(user, unauthorizedUserData.Cart, unauthorizedUserData.Comparison, unauthorizedUserData.Favourite); 
                    }                        
                    if (userInfo.ReturnUrl != null)
                        return Redirect(userInfo.ReturnUrl);                    
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                    ModelState.AddModelError("", "Неправильный пароль");
            }                                           
            return View(userInfo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAsync(UserRegisterInfo userInfo)
        {
            if (userInfo.Login == userInfo.Password)
                ModelState.AddModelError("", "Логин и пароль не должны совпадать!");            
            if (ModelState.IsValid)
            {
                var user = new User { Email = userInfo.Login, UserName = userInfo.Login, RegistrationDate = DateTime.Now };
                var result = await userManager.CreateAsync(user, userInfo.Password);
                if (result.Succeeded)
                {                    
                    await signInManager.SignInAsync(user, false);
                    await TryAssignRoleAsync (user);
                    // получение данных у неавторизованного пользователя 
                    var unauthorizedUserData = await TryGetDataForAuthorizedUserAsync();
                    // перенос корзины, списка сравнения и избранного от неавторизованного пользователя
                    await MoveDataToAuthorizedUserAsync(user, unauthorizedUserData.Cart, unauthorizedUserData.Comparison, unauthorizedUserData.Favourite); 
                    if (userInfo.ReturnUrl != null)
                        return Redirect(userInfo.ReturnUrl);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", $"Ошибка регистрации в поле {error.Description}" );                    
                }           
            }                
            return View(userInfo);
        }


        private async Task TryAssignRoleAsync(User user)
        {
            try
            {
                await userManager.AddToRoleAsync(user, Constants.UserRoleName);
            }
            catch (Exception exc)
            {
                logger.LogInformation($"Не удалось присвоить роль пользователю ошибка {exc.ToString()}");
            }
        }

        public async Task<bool> IsUserExistsAsync(string login)
        {
            return await userManager.FindByEmailAsync(login) != null;
        }

        private async Task<UnauthorizedUserData> TryGetDataForAuthorizedUserAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);            
            var cart = new Cart();
            var comparison = new Comparison();
            var favourite = new Favourite();
            if (user == null)
            {
                var userId = Request.Cookies["Id"];
                cart = await cartRepository.TryGetCartByUserIdAsync(userId);
                comparison = await comparisonRepository.TryGetComparisonByUserIdAsync(userId);
                favourite = await favouritesRepository.TryGetFavoriteProductsListByUserIdAsync(userId);
            }
            return new UnauthorizedUserData { Cart = cart, Comparison = comparison, Favourite = favourite };
        }


        private async Task MoveDataToAuthorizedUserAsync(User user, Cart cart, Comparison comparison, Favourite favourite)
        {
            await cartRepository.MoveDataToAuthorizedUserAsync(user, cart);
            await comparisonRepository.MoveDataToAuthorizedUserAsync(user, comparison);
            await favouritesRepository .MoveDataToAuthorizedUserAsync(user, favourite);
        }


    }
}
