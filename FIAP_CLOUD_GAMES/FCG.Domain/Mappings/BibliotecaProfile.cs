using AutoMapper;
using FCG.Domain.DTO.Responses.BibliotecaResponses;
using FCG.Domain.Entity;

namespace FCG.Domain.Mappings;

public class BibliotecaProfile : Profile
{
    public BibliotecaProfile()
    {
        CreateMap<Biblioteca, BibliotecaResponse>()
            .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Jogo.Titulo))
            .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Jogo.Descricao))
            .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => src.Jogo.Preco));
    }
}
