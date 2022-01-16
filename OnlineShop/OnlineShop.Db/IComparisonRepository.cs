using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Db
{
    public interface IComparisonRepository
    {

        Comparison TryGetComparisonByUserId(string userId);

        void Add(Product product, string userId);

        void Delete(Guid productId, string userId);

        Comparison Clone(Comparison comparison);

        void MoveDataToAuthorizedUser(User user, Comparison comparison);

    }
}
