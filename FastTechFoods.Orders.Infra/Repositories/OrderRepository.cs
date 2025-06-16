using FastTechFoods.Orders.Domain.Entities;
using FastTechFoods.Orders.Domain.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FastTechFoods.Orders.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _order;

        public OrderRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbSettings)
        {
            var database = mongoClient.GetDatabase(mongoDbSettings.Value.Database);
            _order = database.GetCollection<Order>("Order");
        }
        public virtual async Task<List<Order>> GetAll() => await _order.Find(_ => true).ToListAsync();       

    }
}

