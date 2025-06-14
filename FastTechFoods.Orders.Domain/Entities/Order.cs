using System.Text.Json;
using FastTechFoods.Orders.Domain.Enums;

namespace FastTechFoods.Orders.Domain.Entities
{
    public class Order : EntityBase
    {
        public Guid IdUser { get; set; }

        public Guid IdStore { get; set; }

        public OrderStatus Status { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public List<Item> OrderItems { get; set; } = new();

        public Order()
        {

        }

        public Order(Guid idUser, Guid idStore, DeliveryType deliveryType, List<Item> orderItems)
        {
            IdUser = idUser;
            IdStore = idStore;
            DeliveryType = deliveryType;
            OrderItems = orderItems;
        }
    }
}