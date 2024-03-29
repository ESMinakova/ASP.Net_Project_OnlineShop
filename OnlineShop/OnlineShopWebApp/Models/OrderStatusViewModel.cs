﻿using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public enum OrderStatusViewModel
    {
        [Display(Name ="Создан")]
        Created,
        [Display(Name = "Обработан")]
        Processed,
        [Display(Name = "В пути")]
        Delivering,
        [Display(Name = "Выполнен")]
        Done,
        [Display(Name = "Отменён")]
        Canceled
    }
}