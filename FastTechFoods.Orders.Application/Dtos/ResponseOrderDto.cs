namespace FastTechFoods.Orders.Application.Dtos
{
    public class ResponseOrderDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public Guid IdStore { get; set; }        
        public Guid IdUser { get; set; }
        public string Status { get; set; }
        public string DeliveryType { get; set; }        
        public required IEnumerable<ResponseItemDto> Items { get; set; }
    }
}
