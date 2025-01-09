using AutoMapper;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;

namespace VisionShopAPI.Profiles
{
    public class ItemVendaProfile : Profile
    {
        public ItemVendaProfile()
        {
            CreateMap<CreateItemVendaDto, ItemVenda>();
            CreateMap<ItemVenda, ReadItemVendaDto>()
                    .ForMember(dest => dest.NomeOculos, opt => opt.MapFrom(src => src.Oculos.Nome));
        }
    
    }
}
