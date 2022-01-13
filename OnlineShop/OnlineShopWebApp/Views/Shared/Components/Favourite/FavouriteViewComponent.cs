using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using System;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Views.Shared.Components.Favourite
{
    public class FavouriteViewComponent : ViewComponent
    {
        private readonly IFavouritesRepository favouritesRepository;
        private readonly IShop shop;
        private readonly UserManager<User> userManager;


        public FavouriteViewComponent(IFavouritesRepository favouritesRepository, IShop shop, UserManager<User> userManager)
        {
            this.favouritesRepository = favouritesRepository;
            this.shop = shop;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid productId)
        {
            string userId = null;
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            userId = user != null ? user.Id : Request.Cookies["Id"];

            var favourite = await favouritesRepository.TryGetFavoriteProductsListByUserIdAsync(userId);
            var currentProduct = await shop.TryGetProductAsync(productId);
            bool IsFavorite = false;
            (ProductViewModel, bool) isProductFavourite = (currentProduct.ToProductViewModel(), IsFavorite);
            if (favourite != null && favourite.FavouriteProducts.Contains(currentProduct))
                isProductFavourite.Item2 = true;
            return View("Favourite", isProductFavourite);
        }
    }
}
