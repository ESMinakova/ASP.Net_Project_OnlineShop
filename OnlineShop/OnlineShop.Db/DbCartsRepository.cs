using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public class DbCartsRepository : ICartRepository
    {
        private readonly DatabaseContext databaseContext;

        public DbCartsRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }


        public async Task<Cart> TryGetCartByUserIdAsync(string userId)            
        {            
            return await databaseContext.Carts.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task AddAsync(Product product, string userId)
        {
            var existingCart = await TryGetCartByUserIdAsync(userId);
            if (existingCart == null)
            {
                var newCart = new Cart()
                {
                    UserId = userId,
                };
                newCart.Items = new List<CartItem>()
                { 
                    new CartItem() 
                    {                    
                        Amount = 1,
                        Product = product,                        
                    }                     
                };
                databaseContext.Carts.Add(newCart);                
            }
            else
            {
                var existingItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (existingItem == null)
                    existingCart.Items.Add(new CartItem() { Amount = 1, Product = product });
                else existingItem.Amount++;
            }
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid productId, string userId)
        {
            var currentCart = await TryGetCartByUserIdAsync(userId);            
            var productToDelete = currentCart.Items.FirstOrDefault(x => x.Product.Id == productId);
            if (productToDelete.Amount > 1)
                productToDelete.Amount--;
            else
            {
                var currentCartItem = currentCart.Items.FirstOrDefault(x => x.Product.Id == productId);
                currentCart.Items.Remove(currentCartItem);
                databaseContext.CartItems.Remove(currentCartItem);
            }            
            await databaseContext.SaveChangesAsync();
        }
        public async Task ClearAsync(string userId)
        {
            var currentCart = await TryGetCartByUserIdAsync(userId);
            currentCart.Items.Clear();
            await databaseContext.SaveChangesAsync();
        }

        public Cart Clone(Cart cart)
        {                        
            return new Cart { UserId = cart?.UserId, Items = (cart?.Items.Select(x => x).ToList()) };                    
        }

        public async Task MoveDataToAuthorizedUserAsync(User user, Cart cart)
        {
            if (cart != null)
            {
                var newCart = Clone(cart);
                var oldCart = await TryGetCartByUserIdAsync(user.Id);
                if (oldCart != null)
                    oldCart.Items = oldCart.Items.Concat(newCart.Items).Distinct().ToList();
                else
                {
                    newCart.UserId = user.Id;
                    databaseContext.Carts.Add(newCart);
                }
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
