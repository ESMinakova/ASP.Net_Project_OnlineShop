using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Product
    {        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
                
        public List<Image> Images { get; set; }
        public Category Category { get; set; }

        public List<CartItem> CartsItems { get; set; }

        public List<Favourite> Favourites { get; set; }

        public List<Comparison> Comparisons { get; set; }

        public Product()
        {
            CartsItems = new List<CartItem>();
            Favourites = new List<Favourite>();
            Comparisons = new List<Comparison>();
            Images = new List<Image>();
        }
    }
}
