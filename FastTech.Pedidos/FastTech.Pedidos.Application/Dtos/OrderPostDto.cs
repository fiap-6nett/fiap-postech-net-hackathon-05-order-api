using System.ComponentModel.DataAnnotations;
using FastTech.Pedidos.Domain.Entities;
using FastTech.Pedidos.Domain.Enums;

namespace FastTech.Pedidos.Application.Dtos;

public class OrderPostDto
{
    [Required(ErrorMessage = "Id User is required")]
    public Guid IdUser { get; set; }
    
    [Required(ErrorMessage = "Id Store is required")]
    public Guid IdStore { get; set; }
    
    [Required(ErrorMessage = "Menu Item Id is required")]
    public Guid MenuItemId { get; set; }
    
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