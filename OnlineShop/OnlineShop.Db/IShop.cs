using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Db
{
    public interface IShop
    {       

        Product TryGetProduct(Guid productId);

        List<Product> TryGetProducts(string request);        

        List<Product> TryGetProductsByCategory(Category category);

        void Add(Product product);

        void Remove(Product product);

        List<Product> GetAll();

        void Edit(Guid productId, Product product);

    }
}
