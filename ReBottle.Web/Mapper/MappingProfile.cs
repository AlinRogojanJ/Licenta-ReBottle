using AutoMapper;
using ReBottle.Models;
using ReBottle.Models.DTOs;

namespace ReBottle.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserGetDTO>().ReverseMap();
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<Location, LocationGetDTO>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusDTO>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusGetDTO>().ReverseMap();
            CreateMap<RecyclingRecord, RecyclingRecordUpdateDTO>().ReverseMap();
            CreateMap<RecyclingRecord, RecyclingRecordDTO>().ReverseMap();
        }
    }
}
