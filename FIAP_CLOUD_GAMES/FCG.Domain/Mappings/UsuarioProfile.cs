using AutoMapper;
using FCG.Domain.DTO.Requests.Usuario;
using FCG.Domain.DTO.Requests.UsuarioRequests;
using FCG.Domain.DTO.Responses.UsuarioResponses;
using FCG.Domain.Entity;

namespace FCG.Domain.Mappings;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CriarUsuarioRequest, Usuario>();
        CreateMap<AtualizarUsuarioRequest, Usuario>();
        CreateMap<Usuario, UsuarioResponse>();
    }
}
