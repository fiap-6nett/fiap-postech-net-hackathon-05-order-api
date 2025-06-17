using FastTechFoods.Orders.Application.Dtos;
using FastTechFoods.Orders.Application.Interfaces;
using FastTechFoods.Orders.Domain.Entities;

namespace FastTechFoods.Orders.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRabbitMqProducer _rabbitMqProducer;

        public OrderService(IRabbitMqProducer rabbitMqProducer)
        {
            _rabbitMqProducer = rabbitMqProducer;
        }

        public async Task<Guid> SendOrderQueueAsync(OrderDto orderDto)
        {
            try
            {
                Order order = new Order
                {
                    IdStore = orderDto.IdStore,
                    IdUser = orderDto.IdUser,
                    DeliveryType = orderDto.DeliveryType,
                    Items = orderDto.Items.Select(i => new Item(
                        id: i.Id,
                        menuItemId: i.MenuItemId,
                        name: i.Name,
                        description: i.Description,
                        price: i.Price,
                        amount: i.Amount,
                        category: i.Category,
                        notes: i.Notes)
                    )
                };

                await _rabbitMqProducer.SendMessageToQueue(order);

                return order.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send order registration to queue. {ex.Message}");
            }            
        }

        public Task SendOrderChangeStatusAsync(ChangeStatusSendQueueDto pedido)
        {
            try
            {
                _rabbitMqProducer.SendMessageChangeStatusQueue(pedido);

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send order status update to queue. {ex.Message}");
            }
        }
    }
}