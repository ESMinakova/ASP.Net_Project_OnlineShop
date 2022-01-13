using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public class DbFavouritesRepository : IFavouritesRepository
    {
        private readonly DatabaseContext databaseContext;

        public DbFavouritesRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Favourite> TryGetFavoriteProductsListByUserIdAsync(string userId)
        {            
            return await databaseContext.Favourites.Include(x => x.FavouriteProducts).FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task AddAsync(Product product, string userId)
        {
            var existingFavourites = await TryGetFavoriteProductsListByUserIdAsync(userId);
            if (existingFavourites == null)
            {
                var favourites = new Favourite() { UserId = userId, FavouriteProducts = new List<Product> { product } };
                product.Favourites.Add(favourites);
                databaseContext.Favourites.Add(favourites);
            }
            else
            {
                if (!existingFavourites.FavouriteProducts.Contains(product))
                {
                    existingFavourites.FavouriteProducts.Add(product);
                    product.Favourites.Add(existingFavourites);
                }      
            }
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid productId, string userId)
        {
            var existingFavourites = await TryGetFavoriteProductsListByUserIdAsync(userId);
            var productToDelete = existingFavourites.FavouriteProducts.FirstOrDefault(x => x.Id == productId);
            existingFavourites.FavouriteProducts.Remove(productToDelete);
            productToDelete.Favourites.Remove(existingFavourites);
            await databaseContext.SaveChangesAsync();
        }

        public async Task AddOrDeleteAsync(Product product, string userId)
        {
            var existingFavourites = await TryGetFavoriteProductsListByUserIdAsync(userId);
            if (existingFavourites == null || !existingFavourites.FavouriteProducts.Contains(product))
                await AddAsync(product, userId);
            else
                await DeleteAsync(product.Id, userId);            
        }

        public Favourite Clone(Favourite favourite)
        {
            return new Favourite { UserId = favourite?.UserId, FavouriteProducts = (favourite?.FavouriteProducts.Select(x => x).ToList()) };
        }

        public async Task MoveDataToAuthorizedUserAsync(User user, Favourite favourite)
        {
            if (favourite != null)
            {
                var newFavourite = Clone(favourite);
                var oldFavourite = await TryGetFavoriteProductsListByUserIdAsync(user.Id);
                if (oldFavourite != null)
                    oldFavourite.FavouriteProducts = oldFavourite.FavouriteProducts.Concat(newFavourite.FavouriteProducts).Distinct().ToList();  
                else
                {
                    newFavourite.UserId = user.Id;
                    databaseContext.Favourites.Add(newFavourite);
                }
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
