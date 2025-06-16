using FastTechFoods.Orders.Domain.Enums;

namespace FastTechFoods.Orders.Domain.Entities
{
    public class Order: Base
    {
        public Guid IdStore { get; set; }
        public Guid IdUser { get; set; }
        public OrderStatus Status { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public string Justification { get; set; }

        public Order(Guid idStore, Guid idUser, DeliveryType deliveryType, IEnumerable<Item> items)
        {
            IdStore = idStore;
            IdUser = idUser;
            DeliveryType = deliveryType;
            Items = items;
        }

        public Order() { }
    }
}