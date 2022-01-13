using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Не указано название роли")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина названия должна быть не менее 2 символов")]
        [Remote(action: "CheckName", controller: "Roles", ErrorMessage = "Название уже используется")]
        public string Name { get; set; }
    }
}
