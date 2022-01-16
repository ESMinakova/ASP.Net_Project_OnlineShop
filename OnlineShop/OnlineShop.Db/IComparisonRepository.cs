using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public interface IComparisonRepository
    {

        Task<Comparison> TryGetComparisonByUserIdAsync(string userId);

        Task AddAsync(Product product, string userId);

        Task DeleteAsync(Guid productId, string userId);

        Comparison Clone(Comparison comparison);

        Task MoveDataToAuthorizedUserAsync(User user, Comparison comparison);

    }
}
