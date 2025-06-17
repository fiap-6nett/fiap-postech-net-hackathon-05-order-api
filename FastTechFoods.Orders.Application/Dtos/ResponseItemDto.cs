namespace FastTechFoods.Orders.Application.Dtos
{
    public class ResponseItemDto
    {
        public Guid Id { get; set; }
        public Guid MenuItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Notes { get; set; }
    }
}
