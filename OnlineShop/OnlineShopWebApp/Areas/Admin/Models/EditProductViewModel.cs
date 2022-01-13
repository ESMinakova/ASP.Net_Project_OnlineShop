using Microsoft.AspNetCore.Http;
using OnlineShopWebApp.Areas.Catalogue.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class EditProductViewModel
    {
        public Guid Id { get; set; }

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
        
        public List<string> ImagePaths { get; set; }
        public EditProductViewModel()
        {
            ImageFiles = new List<IFormFile>();
            ImagePaths = new List<string>();
        }        
    }
}
