namespace FastTech.Pedidos.Domain.Entities;

public class OrderItem
{
    
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid MenuItemId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public string? Notes { get; private set; }

        
        public OrderItem(Guid id, Guid orderId, Guid menuItemId, int quantity, decimal price, string notes)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantity));

            if (price <= 0)
                throw new ArgumentException("Preço deve ser maior que zero", nameof(price));

            Id = id;
            OrderId = orderId;
            MenuItemId = menuItemId;
            Quantity = quantity;
            Price = price;
            Notes = notes;
        }
    }
    
