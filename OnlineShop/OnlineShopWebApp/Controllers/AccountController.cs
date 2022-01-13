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

        public IActionResult Logout()
        {
            signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckNonExistentLogin(string login)
        {
            if (IsUserExists(login))
                return Json(false);
            return Json(true);
        }


        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckExistentLogin(string login)
        {
            if (!IsUserExists(login))
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
                        MoveDataToAuthorizedUser(user, unauthorizedUserData.Cart, unauthorizedUserData.Comparison, unauthorizedUserData.Favourite); 
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
                    signInManager.SignInAsync(user, false).Wait();
                    TryAssignRole(user);
                    // получение данных у неавторизованного пользователя 
                    var unauthorizedUserData = await TryGetDataForAuthorizedUserAsync();
                    // перенос корзины, списка сравнения и избранного от неавторизованного пользователя
                    MoveDataToAuthorizedUser(user, unauthorizedUserData.Cart, unauthorizedUserData.Comparison, unauthorizedUserData.Favourite); 
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


        private void TryAssignRole(User user)
        {
            try
            {
                userManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();
            }
            catch (Exception exc)
            {
                logger.LogInformation($"Не удалось присвоить роль пользователю ошибка {exc.ToString()}");
            }
        }

        public bool IsUserExists(string login)
        {
            return userManager.FindByEmailAsync(login).Result != null;
        }

        private async Task<UnauthorizedUserData> TryGetDataForAuthorizedUserAsync()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;            
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


        private void MoveDataToAuthorizedUser(User user, Cart cart, Comparison comparison, Favourite favourite)
        {
            cartRepository.MoveDataToAuthorizedUserAsync(user, cart);
            comparisonRepository.MoveDataToAuthorizedUserAsync(user, comparison);
            favouritesRepository.MoveDataToAuthorizedUserAsync(user, favourite);
        }


    }
}
