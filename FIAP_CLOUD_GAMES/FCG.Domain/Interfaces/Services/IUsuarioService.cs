using FCG.Domain.DTO.Requests.Usuario;
using FCG.Domain.DTO.Requests.UsuarioRequests;
using FCG.Domain.DTO.Responses.UsuarioResponses;

namespace FCG.Domain.Interfaces.Services;

public interface IUsuarioService
{
    Task<UsuarioResponse?> BuscarPorId(Guid guid);
    Task<IEnumerable<UsuarioResponse>> BuscarTodos();
    Task Criar(CriarUsuarioRequest request);
    Task Atualizar(AtualizarUsuarioRequest request);
    Task Deletar(Guid guid);
}
