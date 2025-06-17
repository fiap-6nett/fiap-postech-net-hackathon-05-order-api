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
            CreateMap<Order, ResponseOrderDto>()
                .ForMember(dest => dest.DeliveryType, opt => opt.MapFrom(src => src.DeliveryType.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}