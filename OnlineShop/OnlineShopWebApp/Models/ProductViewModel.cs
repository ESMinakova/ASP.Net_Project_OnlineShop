using Microsoft.AspNetCore.Http;
using OnlineShopWebApp.Areas.Catalogue.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp
{
    public class ProductViewModel
    {        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public string Description { get; set; }

        public List<string> ImagePaths { get; set; }

        public CategoryViewModel Category { get; set; }        

        public string ImagePath => ImagePaths.Count == 0 ? "/Content/coverDish.jpeg" : ImagePaths[0];
    }
}
