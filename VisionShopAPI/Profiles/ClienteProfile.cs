using AutoMapper;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;

namespace VisionShopAPI.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<CreateClienteDto, Cliente>();
            CreateMap<Cliente, ReadClienteDto>();
        }
    }
}
