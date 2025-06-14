using FastTechFoods.Orders.Domain.Enums;

namespace FastTechFoods.Orders.Domain.Entities
{
    public class Order :Base
    {
        public Guid IdStore { get; set; }
        public Guid IdUser { get; set; }
        public OrderStatus Status { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public IEnumerable<Item> Items { get; set; }
        
        public Order(Guid idStore, Guid idUser, DeliveryType deliveryType, List<Item> items)
        {
            IdUser = idUser;
            IdStore = idStore;
            DeliveryType = deliveryType;
            Items = items;
        }
    }
}