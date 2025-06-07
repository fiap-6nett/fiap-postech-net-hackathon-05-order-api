using FastTech.Pedidos.Application.Dtos;
using FastTech.Pedidos.Application.Interfaces;
using FastTech.Pedidos.Domain.Entities;
using FastTech.Pedidos.Domain.Enums;

namespace FastTech.Pedidos.Application.Services;

public class OderService : IOrderService
{
    private readonly IRabbitMqProducer _rabbitMqProducer;
    
    public OderService(IRabbitMqProducer rabbitMqProducer)
    {
        _rabbitMqProducer = rabbitMqProducer;
    }
    
    public async Task<Guid> SendOrderQueueAsync(OrderPostDto pedido)
    {
        var order = new Order
        {
            IdStore = pedido.IdStore,
            IdUser = pedido.IdUser,
            Status = OrderStatus.Pendente,
            DeliveryType = pedido.DeliveryType,
        };
            
        order.OrderItems.AddRange(
            pedido.OrderItems.Select(item => new OrderItems
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                MenuItemId = item.MenuItemId,
                Price = item.Price,
                Quantity = item.Quantity
            })
        );
        
         await _rabbitMqProducer.SendMessageToQueue(order);

        return order.Id;
    }
}