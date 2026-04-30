using FCG.Domain.DTO.Requests.Jogo;
using FCG.Domain.DTO.Requests.JogoRequests;
using FCG.Domain.DTO.Responses.JogoResponses;

namespace FCG.Domain.Interfaces.Services;

public interface IJogoService
{
    Task<JogoResponse?> BuscarPorId(Guid guid);
    Task<IEnumerable<JogoResponse>> BuscarTodos();
    Task Criar(CriarJogoRequest request);
    Task Atualizar(AtualizarJogoRequest request);
    Task Deletar(Guid guid);
}