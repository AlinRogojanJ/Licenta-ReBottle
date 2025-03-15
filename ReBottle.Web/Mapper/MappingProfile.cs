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
            CreateMap<User, UserUpdateDTO>().ReverseMap();
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<Location, LocationUpdateDTO>().ReverseMap();
        }
    }
}
