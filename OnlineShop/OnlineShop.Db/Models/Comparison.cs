using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Comparison
    {
        public Guid Id { get; set; }

        public List<Product> ProductsToCompare { get; set; }

        public string UserId { get; set; }

        public Comparison()
        {
            ProductsToCompare = new List<Product>();
        }
    }
}
