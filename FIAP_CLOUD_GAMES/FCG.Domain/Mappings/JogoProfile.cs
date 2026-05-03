using AutoMapper;
using FCG.Domain.DTO.Requests.Jogo;
using FCG.Domain.DTO.Requests.JogoRequests;
using FCG.Domain.DTO.Responses.JogoResponses;
using FCG.Domain.Entity;

namespace FCG.Domain.Mappings;

public class JogoProfile : Profile
{
    public JogoProfile()
    {
        CreateMap<CriarJogoRequest, Jogo>();
        CreateMap<AtualizarJogoRequest, Jogo>();
        CreateMap<Jogo, JogoResponse>();
        CreateMap<Jogo, JogoResponse>()
            .ForMember(dest => dest.PrecoAtual, opt => opt.MapFrom(src => src.PrecoAtual))
            .ForMember(dest => dest.EmPromocao, opt => opt.MapFrom(src => src.EmPromocao))
            .ForMember(dest => dest.PromocaoExpiracao, opt => opt.MapFrom(src => src.PromocaoExpiracao));
    }
}
