using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class ChangePassword
    {
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть не менее 6 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

    }
}
