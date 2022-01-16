using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Db
{
    public interface IOrderRepository
    {
        void Add(OrderWithContacts order);

        OrderWithContacts TryGetOrderById(Guid orderId);

        void SelectStatus(Guid orderId, OrderStatus newStatus);

        List<OrderWithContacts> GetAll();
    }
}
