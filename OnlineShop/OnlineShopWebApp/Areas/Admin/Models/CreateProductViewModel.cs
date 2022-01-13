using Microsoft.AspNetCore.Http;
using OnlineShopWebApp.Areas.Catalogue.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class CreateProductViewModel
    {        
        
        [Required(ErrorMessage = "Не указано название блюда")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина названия должна быть не менее 2 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана стоимость")]
        [Range(0, 2000, ErrorMessage = "Некорректная стоимость блюда")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Описание блюда не должно быть пустым")]
        [StringLength(300, MinimumLength = 10, ErrorMessage = "Длина описания должна быть от 10 до 300 символов")]
        public string Description { get; set; }       

        public CategoryViewModel Category { get; set; }

        public List<IFormFile> ImageFiles { get; set; }

        public CreateProductViewModel()
        {
            ImageFiles = new List<IFormFile>();
        }
    }
}
