using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Orders.Domain.Enums {
    public enum OrderStatus
    {
        [Display(Name = "Created")]
        Created = 0,

        [Display(Name = "Accepted")]
        Accepted = 1,

        [Display(Name = "Rejected")]
        Rejected = 2,

        [Display(Name = "Cancelled")]
        Cancelled = 3,

        [Display(Name = "Finished")]
        Finished = 4,

        [Display(Name = "Failed")]
        Failed = 5
    }
}