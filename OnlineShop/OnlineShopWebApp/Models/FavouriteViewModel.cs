using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class FavouriteViewModel
    {

        public List<ProductViewModel> FavouriteProducts;

        public string UserId { get; set; }
    }
}
