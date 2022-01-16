using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public class DbShop : IShop
    {
        private readonly DatabaseContext databaseContext;

        public DbShop(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }       

        public async Task<Product> TryGetProductAsync(Guid productId) 
        {
            return await databaseContext.Products.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<List<Product>> TryGetProductsAsync(string request)
        {
            return await databaseContext.Products.Where(x => x.Name.ToLower().Contains(request.ToLower())).ToListAsync();
        }

        public async Task<List<Product>> TryGetProductsByCategoryAsync(Category category)
        {
            return await databaseContext.Products.Where(x => x.Category == category).Include(x => x.Images).ToListAsync();
        }

        public async Task AddAsync(Product product)
        {            
            databaseContext.Products.Add(product);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await databaseContext.Products.Include(x => x.Images).ToListAsync<Product>();
        }

        public async Task EditAsync(Guid productId, Product product)
        {
            var currentProduct = await TryGetProductAsync(productId);
            currentProduct.Name = product.Name;
            currentProduct.Cost = product.Cost;
            currentProduct.Description = product.Description;
            currentProduct.Category = product.Category;
            currentProduct.Images = product.Images;
            await databaseContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Product product)       
        {            
            databaseContext.Products.Remove(product);
            await databaseContext.SaveChangesAsync();
        }
    }
}
