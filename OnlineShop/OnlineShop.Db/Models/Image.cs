using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Db.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Path { get; set; }

        public Guid? ProductId { get; set; }

        public Product Product { get; set; }
    }
}
