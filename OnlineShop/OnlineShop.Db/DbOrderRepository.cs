using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public class DbOrderRepository : IOrderRepository
    {
        private readonly DatabaseContext databaseContext;

        public DbOrderRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task AddAsync(OrderWithContacts order)
        {
            databaseContext.Orders.Add(order);          
            await databaseContext.SaveChangesAsync();
        }

        public async Task<OrderWithContacts> TryGetOrderByIdAsync(Guid orderId)
        {
            return await databaseContext.Orders.Include(x => x.UserDeliveryInfo).Include(x => x.Cart).ThenInclude(x => x.Items)
                .ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task SelectStatusAsync(Guid orderId, OrderStatus newStatus)
        {
            var currentOrder = await TryGetOrderByIdAsync(orderId);
            if (currentOrder != null)
                currentOrder.Status = newStatus;
            await databaseContext.SaveChangesAsync();
        }

        public async Task<List<OrderWithContacts>> GetAllAsync()
        {
            return await databaseContext .Orders.Include(x => x.UserDeliveryInfo).Include(x => x.Cart).ThenInclude(x => x.Items)
                .ThenInclude(x => x.Product).ToListAsync();
        }
    }
}
