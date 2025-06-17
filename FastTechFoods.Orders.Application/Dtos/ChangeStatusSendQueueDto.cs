using FastTechFoods.Orders.Domain.Enums;

namespace FastTechFoods.Orders.Application.Dtos
{
    public class ChangeStatusSendQueueDto
    {
        public Guid OrderId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string Justification { get; set; }
    }
}