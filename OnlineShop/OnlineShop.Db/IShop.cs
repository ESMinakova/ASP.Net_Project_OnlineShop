using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public interface IShop
    {

        Task<Product> TryGetProductAsync(Guid productId);

        Task<List<Product>> TryGetProductsAsync(string request);

        Task<List<Product>> TryGetProductsByCategoryAsync(Category category);

        Task AddAsync(Product product);

        Task RemoveAsync(Product product);

        Task<List<Product>> GetAllAsync();

        Task EditAsync(Guid productId, Product product);

    }
}
