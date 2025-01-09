using AutoMapper;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;

namespace VisionShopAPI.Profiles
{
    public class VendaProfile : Profile
    {
        public VendaProfile()
        {
            CreateMap<CreateVendaDto, Venda>();
            CreateMap<Venda, ReadVendaDto>()
                    .ForMember(dest => dest.ClienteNome, opt => opt.MapFrom(src => src.Cliente.Nome))
                    .ForMember(dest => dest.TotalVenda, opt => opt.MapFrom(src => src.Itens.Sum(i => i.PrecoUnitario * i.Quantidade)));
        }
    }
}
