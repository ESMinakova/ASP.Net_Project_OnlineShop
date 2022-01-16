using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public interface IOrderRepository
    {
        Task AddAsync(OrderWithContacts order);

        Task<OrderWithContacts> TryGetOrderByIdAsync(Guid orderId);

        Task SelectStatusAsync(Guid orderId, OrderStatus newStatus);

        Task<List<OrderWithContacts>> GetAllAsync();
    }
}
