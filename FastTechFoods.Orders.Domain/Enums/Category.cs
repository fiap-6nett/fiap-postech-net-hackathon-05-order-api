﻿using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Orders.Domain.Enums
{
    public enum Category
    {
        [Display(Name = "Snack")]
        Snack = 0,

        [Display(Name = "Drink")]
        Drink = 1,

        [Display(Name = "Dessert")]
        Dessert = 2        
    }
}
