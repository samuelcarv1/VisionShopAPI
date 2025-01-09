using AutoMapper;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;

namespace VisionShopAPI.Profiles
{
    public class OculosProfile : Profile
    {
        public OculosProfile() 
        {
            CreateMap<CreateOculosDto, Oculos>();
            CreateMap<Oculos, ReadOculosDto>();
        }
    }
}
