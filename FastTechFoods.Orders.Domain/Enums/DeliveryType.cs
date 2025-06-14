using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Orders.Domain.Enums
{
    public enum DeliveryType
    {
        [Display(Name = "Counter")]
        Counter = 0,

        [Display(Name = "Drive-thru")]
        Delivery = 1,

        [Display(Name = "Delivery")]
        DriveThru = 2
    }
}