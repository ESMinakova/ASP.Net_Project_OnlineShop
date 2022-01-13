using System;

namespace OnlineShop.Db.Models
{
    public class OrderWithContacts
    {
        public Guid Id { get; set; }

        public UserDeliveryInfo UserDeliveryInfo { get; set; }

        public Cart Cart { get; set; }

        public DateTime OrderTime { get; set; }

        public OrderStatus Status { get; set; } 
    }
}
