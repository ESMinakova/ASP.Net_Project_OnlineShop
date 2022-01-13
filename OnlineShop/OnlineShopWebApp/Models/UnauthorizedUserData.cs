using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Models
{
    public class UnauthorizedUserData
    {
        public Cart Cart { get; set; }

        public Comparison Comparison { get; set; }

        public Favourite Favourite { get; set; }

    }
}
