using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Favourite
    {
        public Guid Id { get; set; }

        public List<Product> FavouriteProducts { get; set; }

        public string UserId { get; set; }

        public Favourite()
        {
            FavouriteProducts = new List<Product>();
        }

    }
}
