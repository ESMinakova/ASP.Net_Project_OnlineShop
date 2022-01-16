using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public interface IFavouritesRepository
    {

        Task<Favourite> TryGetFavoriteProductsListByUserIdAsync(string userId);

        Task AddAsync(Product product, string userId);

        Task DeleteAsync(Guid productId, string userId);

        Task AddOrDeleteAsync(Product product, string userId);

        Favourite Clone(Favourite favourite);

        Task MoveDataToAuthorizedUserAsync(User user, Favourite favourite);
    }
}
