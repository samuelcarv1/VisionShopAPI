using AutoMapper;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;

public class MovimentacaoEstoqueProfile : Profile
{
    public MovimentacaoEstoqueProfile()
    {
        CreateMap<CreateMovimentacaoEstoqueDto, MovimentacaoEstoque>();

        CreateMap<MovimentacaoEstoque, ReadMovimentacaoEstoqueDto>()
                   .ForMember(dest => dest.Oculos, opt => opt.MapFrom(src => src.Oculos));

        //CreateMap<Oculos, ReadOculosDto>();
    }
}
