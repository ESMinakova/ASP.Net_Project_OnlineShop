using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public class DbComparisonRepository : IComparisonRepository
    {
        private readonly DatabaseContext databaseContext;

        public DbComparisonRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Comparison> TryGetComparisonByUserIdAsync(string userId)
        {
            return await databaseContext.Comparisons.Include(x => x.ProductsToCompare).FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task AddAsync(Product product, string userId)
        {
            var comparison = await TryGetComparisonByUserIdAsync(userId);
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
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid productId, string userId)
        {
            var comparison = await TryGetComparisonByUserIdAsync(userId);
            var productToDelete = comparison.ProductsToCompare.FirstOrDefault(x => x.Id == productId);
            productToDelete.Comparisons.Remove(comparison);
            comparison.ProductsToCompare.Remove(productToDelete);
            await databaseContext.SaveChangesAsync();
        }

        public Comparison Clone(Comparison comparison)
        {
            return new Comparison { UserId = comparison?.UserId, ProductsToCompare = (comparison?.ProductsToCompare.Select(x => x).ToList()) };
        }

        public async Task MoveDataToAuthorizedUserAsync(User user, Comparison comparison)
        {
            if (comparison != null)
            {
                var newComparison = Clone(comparison);
                var oldComparison = await TryGetComparisonByUserIdAsync(user.Id);
                if (oldComparison != null)
                    oldComparison.ProductsToCompare = oldComparison.ProductsToCompare.Concat(newComparison.ProductsToCompare).Distinct().ToList();
                else
                {
                    newComparison.UserId = user.Id;
                    databaseContext.Comparisons.Add(newComparison);
                }
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
