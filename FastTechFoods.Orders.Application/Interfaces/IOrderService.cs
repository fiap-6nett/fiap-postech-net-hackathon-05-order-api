using FastTechFoods.Orders.Application.Dtos;
using FastTechFoods.Orders.Domain.Entities;

namespace FastTechFoods.Orders.Application.Interfaces;

public interface IOrderService
{
    Task<Guid> SendOrderQueueAsync(OrderDto pedido);
    Task SendOrderChangeStatusAsync(ChangeStatusSendQueueDto pedido);
}