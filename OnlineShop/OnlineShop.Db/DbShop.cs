using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public class DbShop : IShop
    {
        private readonly DatabaseContext databaseContext;

        public DbShop(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }       

        public Product TryGetProduct(Guid productId) 
        {
            return databaseContext.Products.Include(x => x.Images).FirstOrDefault(x => x.Id == productId);
        }

        public List<Product> TryGetProducts(string request)
        {
            return databaseContext.Products.Where(x => x.Name.ToLower().Contains(request.ToLower())).ToList();
        }

        public List<Product> TryGetProductsByCategory(Category category)
        {
            return databaseContext.Products.Where(x => x.Category == category).ToList();
        }

        public void Add(Product product)
        {            
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return databaseContext.Products.Include(x => x.Images).ToList();
        }

        public void Edit(Guid productId, Product product)
        {
            var currentProduct = TryGetProduct(productId);
            currentProduct.Name = product.Name;
            currentProduct.Cost = product.Cost;
            currentProduct.Description = product.Description;
            currentProduct.Category = product.Category;
            currentProduct.Images = product.Images;
            databaseContext.SaveChanges();
        }

        public void Remove(Product product)       
        {            
            databaseContext.Products.Remove(product);
            databaseContext.SaveChanges();
        }
    }
}
