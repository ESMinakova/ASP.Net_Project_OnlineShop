using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public class DbOrderRepository : IOrderRepository
    {
        private readonly DatabaseContext databaseContext;

        public DbOrderRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public void Add(OrderWithContacts order)
        {
            databaseContext.Orders.Add(order);          
            databaseContext.SaveChanges();
        }

        public OrderWithContacts TryGetOrderById(Guid orderId)
        {
            return databaseContext.Orders.Include(x => x.UserDeliveryInfo).Include(x => x.Cart).ThenInclude(x => x.Items)
                .ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == orderId);
        }

        public void SelectStatus(Guid orderId, OrderStatus newStatus)
        {
            var currentOrder = TryGetOrderById(orderId);
            if (currentOrder != null)
                currentOrder.Status = newStatus;
            databaseContext.SaveChanges();
        }

        public List<OrderWithContacts> GetAll()
        {
            return databaseContext.Orders.Include(x => x.UserDeliveryInfo).Include(x => x.Cart).ThenInclude(x => x.Items)
                .ThenInclude(x => x.Product).ToList();
        }
    }
}
