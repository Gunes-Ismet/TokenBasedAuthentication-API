using AuthServer.Core.DTO_s;
using AuthServer.Core.Entities;
using AutoMapper;

namespace AuthServer.Service.AutoMapper
{
    class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<UserApp, UserAppDTO>().ReverseMap();
        }
    }
}
