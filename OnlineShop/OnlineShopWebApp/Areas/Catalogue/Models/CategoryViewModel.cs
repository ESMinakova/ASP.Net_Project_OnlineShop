using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Catalogue.Models
{
    public enum CategoryViewModel
    {
        [Display(Name = "Завтрак")]
        Breakfast,

        [Display(Name = "Салат")]
        Salad,

        [Display(Name = "Суп")]
        Soup,

        [Display(Name = "Горячее блюдо")]
        MainDish,

        [Display(Name = "Гарнир")]
        SideDish,

        [Display(Name = "Выпечка")]
        Bakery,

        [Display(Name = "Напиток")]
        Drink
    }
}
