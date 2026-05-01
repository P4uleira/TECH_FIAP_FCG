using FCG.Domain.DTO.Requests.Jogo;
using FCG.Domain.DTO.Requests.JogoRequests;
using FCG.Domain.DTO.Requests.Usuario;
using FCG.Domain.DTO.Requests.UsuarioRequests;
using FCG.Domain.DTO.Responses.JogoResponses;
using FCG.Domain.DTO.Responses.UsuarioResponses;

namespace FCG.Application.Interfaces;

public interface IUsuarioHandler
{
    Task<UsuarioResponse?> BuscarPorId(Guid id);
    Task<IEnumerable<UsuarioResponse>> BuscarTodos();
    Task Criar(CriarUsuarioRequest request);
    Task Atualizar(AtualizarUsuarioRequest request);
    Task Deletar(Guid id);
}
