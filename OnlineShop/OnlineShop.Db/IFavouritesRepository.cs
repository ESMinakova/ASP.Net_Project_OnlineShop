using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Db
{
    public interface IFavouritesRepository
    {

        Favourite TryGetFavoriteProductsListByUserId(string userId);

        void Add(Product product, string userId);

        void Delete(Guid productId, string userId);

        void AddOrDelete(Product product, string userId);

        Favourite Clone(Favourite favourite);

        void MoveDataToAuthorizedUser(User user, Favourite favourite);
    }
}
