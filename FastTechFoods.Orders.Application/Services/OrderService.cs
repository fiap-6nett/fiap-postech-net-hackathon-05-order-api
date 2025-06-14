using FastTechFoods.Orders.Application.Dtos;
using FastTechFoods.Orders.Application.Interfaces;
using FastTechFoods.Orders.Domain.Entities;
using FastTechFoods.Orders.Domain.Enums;

namespace FastTechFoods.Orders.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRabbitMqProducer _rabbitMqProducer;

        public OrderService(IRabbitMqProducer rabbitMqProducer)
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
}