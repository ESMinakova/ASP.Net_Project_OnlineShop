using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserSignInInfo
    {
        [Required(ErrorMessage ="Не указан адрес электронной почты")]
        [EmailAddress(ErrorMessage ="Введен некорректный адрес email")]        
        [Remote(action: "CheckExistentLogin", controller: "Account", ErrorMessage = "Пользователеь с таким почтовым адресом не найден")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть не менее 6 символов")]
        public string Password { get; set; }
        public bool IsMemorize { get; set; }

        public string ReturnUrl { get; set; } 
    }
}
