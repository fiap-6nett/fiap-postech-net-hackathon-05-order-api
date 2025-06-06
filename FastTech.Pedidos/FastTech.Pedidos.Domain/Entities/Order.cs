using System.Text.Json;
using FastTech.Pedidos.Domain.Enums;

namespace FastTech.Pedidos.Domain.Entities;

public class Order : EntityBase
{
    public Guid IdUser { get; set; }
    
    public Guid IdStore { get; set; }

    public string? Justification { get; set; }
    
    public OrderStatus Status { get; set; }
    
    public DeliveryType DeliveryType { get; set; }

    public List<OrderItems> OrderItems { get; set; } = new();
}