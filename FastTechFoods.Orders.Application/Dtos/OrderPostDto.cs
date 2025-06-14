using System.ComponentModel.DataAnnotations;
using FastTechFoods.Orders.Domain.Entities;
using FastTechFoods.Orders.Domain.Enums;

namespace FastTechFoods.Orders.Application.Dtos
{
    public class OrderPostDto
    {
        [Required(ErrorMessage = "Id User is required")]
        public Guid IdUser { get; set; }

        [Required(ErrorMessage = "Id Store is required")]
        public Guid IdStore { get; set; }

        [Required(ErrorMessage = "Delivery Type is required")]
        public DeliveryType DeliveryType { get; set; }

        [Required(ErrorMessage = "Order Item is required")]
        public List<OrderItemsDto> OrderItems { get; set; }
    }

    public class OrderItemsDto
    {
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public string Notes { get; set; }
    }
}