using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserDeliveryInfoViewModel
    {

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина имени должна быть не менее 2 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона")]
        [RegularExpression(@"^((\+7|7|8)+([0-9]){10})$", ErrorMessage ="Некорректный номер телефона")]
        public string Phone { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }

        [Required(ErrorMessage = "Выберите способ оплаты")]
        public string Pay { get; set; }
        public string Comments { get; set; }

        public bool IsAgree { get; set; }
    }
}
