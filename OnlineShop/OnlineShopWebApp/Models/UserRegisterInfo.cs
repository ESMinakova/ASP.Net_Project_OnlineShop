using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserRegisterInfo
    {
        [Required(ErrorMessage = "Не указан адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Введен некорректный адрес email")]        
        [Remote(action: "CheckNonExistentLogin", controller: "Account", ErrorMessage = "Пользователеь с таким почтовым адресом уже зарегистрирован")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [StringLength(20, MinimumLength = 6, ErrorMessage ="Длина пароля должна быть не менее 6 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage ="Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 50 символов")]
        public string Name { get; set; }

        [RegularExpression(@"^((\+7|7|8)+([0-9]){10})$", ErrorMessage = "Некорректный номер телефона")]
        public string Phone { get; set; }

        public string ReturnUrl { get; set; }       
    }
}
