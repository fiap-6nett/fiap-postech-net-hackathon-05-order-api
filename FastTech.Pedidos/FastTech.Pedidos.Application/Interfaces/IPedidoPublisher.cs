using FastTech.Pedidos.Domain.Entities;

namespace FastTech.Pedidos.Application.Interfaces;

public interface IPedidoPublisher
{
    Task SendOrderQueueAsync(Order pedido);
}