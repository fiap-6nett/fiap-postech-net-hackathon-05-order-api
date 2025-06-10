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
        Order order;
        
        try
        {
            order = new Order
            {
                IdStore = pedido.IdStore,
                IdUser = pedido.IdUser,
                Status = OrderStatus.Created,
                DeliveryType = pedido.DeliveryType
            };

            order.OrderItems.AddRange(
                pedido.OrderItems.Select(item =>
                    new OrderItem(
                        Guid.NewGuid(),
                        order.Id,
                        item.MenuItemId,
                        item.Quantity,
                        item.Price,
                        item.Notes
                    )
                )
            );
        }
        catch (Exception ex)
        {
            throw new Exception($"Falha na construção da entidade Oder. {ex.Message}");
        }
        
        await _rabbitMqProducer.SendMessageToQueue(order);
        
        return order.Id;
    }
}