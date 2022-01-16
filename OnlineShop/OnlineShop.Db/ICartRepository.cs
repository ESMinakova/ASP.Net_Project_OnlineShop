using OnlineShop.Db.Models;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public interface ICartRepository
    {

        Task<Cart> TryGetCartByUserIdAsync(string userId);

        Task AddAsync(Product product, string userId);

        Task DeleteAsync(Guid productId, string userId);

        Task ClearAsync(string userId);

        Cart Clone(Cart cart);

        Task MoveDataToAuthorizedUserAsync(User user, Cart cart);
    }
}
