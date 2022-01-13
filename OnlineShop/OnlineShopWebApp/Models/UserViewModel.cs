using OnlineShopWebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set;  }        

        public string Login { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть не менее 6 символов")]
        public string Password { get; set; }

        public RoleViewModel Role { get; set; }

        public DateTime RegistrationDate { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 50 символов")]
        public string Name { get; set; }

        [RegularExpression(@"^((\+7|7|8)+([0-9]){10})$", ErrorMessage = "Некорректный номер телефона")]
        public string Phone { get; set; }

        public UserViewModel()
        {
            Id = Guid.NewGuid();
            RegistrationDate = DateTime.Now;            
        }
    }
}
