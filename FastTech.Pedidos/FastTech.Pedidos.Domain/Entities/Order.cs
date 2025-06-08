using System.Text.Json;
using FastTech.Pedidos.Domain.Enums;

namespace FastTech.Pedidos.Domain.Entities;

public class Order : EntityBase
{
    public Guid IdUser { get; set; }
    
    public Guid IdStore { get; set; }

    public OrderStatus Status { get; set; }
    
    public DeliveryType DeliveryType { get; set; }
    
    public List<OrderItem> OrderItems { get; set; } = new();

    public Order()
    {
        
    }

    public Order(Guid idUser, Guid idStore, DeliveryType deliveryType, List<OrderItem> orderItems)
    {
        IdUser = idUser;
        IdStore = idStore;
        DeliveryType = deliveryType;
        OrderItems = orderItems;
    }
}