using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public class DbComparisonRepository : IComparisonRepository
    {
        private readonly DatabaseContext databaseContext;

        public DbComparisonRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Comparison TryGetComparisonByUserId(string userId)
        {
            return databaseContext.Comparisons.Include(x => x.ProductsToCompare).FirstOrDefault(x => x.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var comparison = TryGetComparisonByUserId(userId);
            if (comparison == null)
            {
                comparison = new Comparison() { ProductsToCompare = new List<Product>(), UserId = userId };
                comparison.ProductsToCompare.Add(product);
                product.Comparisons.Add(comparison);
                databaseContext.Comparisons.Add(comparison);
            }
            else
            {
                if (!comparison.ProductsToCompare.Contains(product))
                {
                    comparison.ProductsToCompare.Add(product);
                    product.Comparisons.Add(comparison);
                }
            }
            databaseContext.SaveChanges();
        }

        public void Delete(Guid productId, string userId)
        {
            var comparison = TryGetComparisonByUserId(userId);
            var productToDelete = comparison.ProductsToCompare.FirstOrDefault(x => x.Id == productId);
            productToDelete.Comparisons.Remove(comparison);
            comparison.ProductsToCompare.Remove(productToDelete);
            databaseContext.SaveChanges();
        }

        public Comparison Clone(Comparison comparison)
        {
            return new Comparison { UserId = comparison?.UserId, ProductsToCompare = (comparison?.ProductsToCompare.Select(x => x).ToList()) };
        }

        public void MoveDataToAuthorizedUser(User user, Comparison comparison)
        {
            if (comparison != null)
            {
                var newComparison = Clone(comparison);
                var oldComparison = TryGetComparisonByUserId(user.Id);
                if (oldComparison != null)
                    oldComparison.ProductsToCompare = oldComparison.ProductsToCompare.Concat(newComparison.ProductsToCompare).Distinct().ToList();
                else
                {
                    newComparison.UserId = user.Id;
                    databaseContext.Comparisons.Add(newComparison);
                }
                databaseContext.SaveChanges();
            }
        }
    }
}
