using System;

namespace OnlineShopWebApp.Models
{
    public class OrderWithContactsViewModel
    {
        public Guid Id { get; set; }

        public UserDeliveryInfoViewModel UserDeliveryInfo { get; set; } 

        public CartViewModel Cart { get; set; }

        public DateTime OrderTime { get; set; }

        public OrderStatusViewModel Status { get; set; }        

    }
}
