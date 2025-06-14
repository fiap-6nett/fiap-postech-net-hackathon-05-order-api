using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Orders.Domain.Enums {
    public enum OrderStatus
    {
        [Display(Name = "Created")]
        Created = 0,

        [Display(Name = "Cancelled")]
        Cancelled = 1,

        [Display(Name = "Accepted")]
        Accepted = 2,

        [Display(Name = "Rejected")]
        Rejected = 3,

        [Display(Name = "InProgress")]
        InProgress = 4,

        [Display(Name = "Finished")]
        Finished = 5
    }
}