using FastTechFoods.Orders.Domain.Enums;

namespace FastTechFoods.Orders.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; private set; }        
        public Guid MenuItemId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public Category Category { get; set; }
        public string Notes { get; set; }


        public Item(Guid id, Guid menuItemId, string name, string description, decimal price, decimal amount, Category category, string notes)
        {
            if (amount <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(amount));

            if (price <= 0)
                throw new ArgumentException("Preço deve ser maior que zero", nameof(price));

            Id = id;            
            MenuItemId = menuItemId;
            Name = name;
            Description = description;
            Price = price;
            Amount = amount;
            Category = category;
            Notes = notes;
        }
    }
}
