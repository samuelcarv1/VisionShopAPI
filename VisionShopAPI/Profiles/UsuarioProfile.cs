using AutoMapper;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;

namespace VisionShopAPI.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<LoginDto, Usuario>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));
        }
    
    }
}
