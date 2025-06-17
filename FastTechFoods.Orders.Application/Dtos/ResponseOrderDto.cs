namespace FastTechFoods.Orders.Application.Dtos
{
    public class ResponseOrderDto
    {
        
        public Guid IdStore { get; set; }        
        public Guid IdUser { get; set; }
        public string Status { get; set; }
        public string DeliveryType { get; set; }        
        public required IEnumerable<ResponseItemDto> Items { get; set; }
    }
}
