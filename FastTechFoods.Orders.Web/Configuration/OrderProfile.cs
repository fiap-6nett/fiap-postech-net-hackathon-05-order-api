using AutoMapper;
using FastTechFoods.Orders.Domain.Entities;
using FastTechFoods.Orders.Application.Dtos;

namespace FastTechFoods.Orders.Web.Configuration.Mappings
{    
    public class OrderProfile : Profile
    {       
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>();
        }
    }
}