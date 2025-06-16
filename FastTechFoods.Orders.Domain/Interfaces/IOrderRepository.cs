using FastTechFoods.Orders.Domain.Entities;

namespace FastTechFoods.Orders.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();        
    }
}