using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public class DbFavouritesRepository : IFavouritesRepository
    {
        private readonly DatabaseContext databaseContext;

        public DbFavouritesRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Favourite TryGetFavoriteProductsListByUserId(string userId)
        {
            var favourite = databaseContext.Favourites.Include(x => x.FavouriteProducts).FirstOrDefault(x => x.UserId == userId);
            return favourite;
        }

        public void Add(Product product, string userId)
        {
            var existingFavourites = TryGetFavoriteProductsListByUserId(userId);
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
            databaseContext.SaveChanges();
        }

        public void Delete(Guid productId, string userId)
        {
            var existingFavourites = TryGetFavoriteProductsListByUserId(userId);
            var productToDelete = existingFavourites.FavouriteProducts.FirstOrDefault(x => x.Id == productId);
            existingFavourites.FavouriteProducts.Remove(productToDelete);
            productToDelete.Favourites.Remove(existingFavourites);
            databaseContext.SaveChanges();
        }

        public void AddOrDelete(Product product, string userId)
        {
            var existingFavourites = TryGetFavoriteProductsListByUserId(userId);
            if (existingFavourites == null || !existingFavourites.FavouriteProducts.Contains(product))
                Add(product, userId);
            else
                Delete(product.Id, userId);            
        }

        public Favourite Clone(Favourite favourite)
        {
            return new Favourite { UserId = favourite?.UserId, FavouriteProducts = (favourite?.FavouriteProducts.Select(x => x).ToList()) };
        }

        public void MoveDataToAuthorizedUser(User user, Favourite favourite)
        {
            if (favourite != null)
            {
                var newFavourite = Clone(favourite);
                var oldFavourite = TryGetFavoriteProductsListByUserId(user.Id);
                if (oldFavourite != null)
                    oldFavourite.FavouriteProducts = oldFavourite.FavouriteProducts.Concat(newFavourite.FavouriteProducts).Distinct().ToList();  
                else
                {
                    newFavourite.UserId = user.Id;
                    databaseContext.Favourites.Add(newFavourite);
                }
                databaseContext.SaveChanges();
            }
        }
    }
}
