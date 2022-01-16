using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public class DbCartsRepository : ICartRepository
    {
        private readonly DatabaseContext databaseContext;

        public DbCartsRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }


        public Cart TryGetCartByUserId(string userId)            
        {            
            return databaseContext.Carts.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var existingCart = TryGetCartByUserId(userId);
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
            databaseContext.SaveChanges();
        }

        public void Delete(Guid productId, string userId)
        {
            var currentCart = TryGetCartByUserId(userId);            
            var productToDelete = currentCart.Items.FirstOrDefault(x => x.Product.Id == productId);
            if (productToDelete.Amount > 1)
                productToDelete.Amount--;
            else
            {
                var currentCartItem = currentCart.Items.FirstOrDefault(x => x.Product.Id == productId);
                currentCart.Items.Remove(currentCartItem);
                databaseContext.CartItems.Remove(currentCartItem);
            }            
            databaseContext.SaveChanges();
        }
        public void Clear(string userId)
        {
            var currentCart = TryGetCartByUserId(userId);
            currentCart.Items.Clear();
            databaseContext.SaveChanges();
        }

        public Cart Clone(Cart cart)
        {                        
            return new Cart { UserId = cart?.UserId, Items = (cart?.Items.Select(x => x).ToList()) };                      

        }

        public void MoveDataToAuthorizedUser(User user, Cart cart)
        {
            if (cart != null)
            {
                var newCart = Clone(cart);
                var oldCart = TryGetCartByUserId(user.Id);
                if (oldCart != null)
                    oldCart.Items = oldCart.Items.Concat(newCart.Items).Distinct().ToList();
                else
                {
                    newCart.UserId = user.Id;
                    databaseContext.Carts.Add(newCart);
                }
                databaseContext.SaveChanges();
            }
        }
    }
}
