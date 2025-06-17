namespace FastTechFoods.Orders.Application.Dtos
{
    public class ChangeStatusDto
    {
        public Guid OrderId { get; set; }
        
        public string Justification { get; set; }
    }
}