namespace FastTech.Pedidos.Domain.Entities;

public class OrderItems
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid MenuItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    public string Notes { get; set; }
    
}