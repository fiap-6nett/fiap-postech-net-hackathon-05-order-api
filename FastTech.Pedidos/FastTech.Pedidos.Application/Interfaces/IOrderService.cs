using FastTech.Pedidos.Application.Dtos;
using FastTech.Pedidos.Domain.Entities;

namespace FastTech.Pedidos.Application.Interfaces;

public interface IOrderService
{
    Task<Guid> SendOrderQueueAsync(OrderPostDto pedido);
}