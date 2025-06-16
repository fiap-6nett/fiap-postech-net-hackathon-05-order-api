using System.ComponentModel.DataAnnotations;
using FastTechFoods.Orders.Domain.Enums;

namespace FastTechFoods.Orders.Application.Dtos
{
    public class OrderDto
    {
        [Required(ErrorMessage = "Id Store is required")]
        public Guid IdStore { get; set; }

        [Required(ErrorMessage = "Id User is required")]
        public Guid IdUser { get; set; }
                
        [Required(ErrorMessage = "Delivery Type is required")]
        public DeliveryType DeliveryType { get; set; }

        [Required(ErrorMessage = "Order Item is required")]
        public required IEnumerable<ItemDto> Items { get; set; }
    }
}
