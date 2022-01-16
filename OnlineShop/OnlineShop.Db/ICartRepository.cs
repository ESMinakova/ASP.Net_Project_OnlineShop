using OnlineShop.Db.Models;
using System;

namespace OnlineShop.Db
{
    public interface ICartRepository
    {    

        Cart TryGetCartByUserId(string userId);

        void Add(Product product, string userId);

        void Delete(Guid productId, string userId);

        void Clear(string userId);

        Cart Clone(Cart cart);

        void MoveDataToAuthorizedUser(User user, Cart cart);
    }
}
