using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models
{
    public class UserDeliveryInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
        public string Pay { get; set; }
        public string Comments { get; set; }
        
    }
}
