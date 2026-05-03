using FCG.Domain.DTO.Requests.Jogo;
using FCG.Domain.DTO.Requests.JogoRequests;
using FCG.Domain.DTO.Responses.JogoResponses;

namespace FCG.Application.Interfaces;

public interface IJogoHandler
{
    Task<JogoResponse?> BuscarPorId(Guid id);
    Task<IEnumerable<JogoResponse>> BuscarTodos();
    Task Criar(CriarJogoRequest request);
    Task Atualizar(AtualizarJogoRequest request);
    Task Deletar(Guid id);
    Task AplicarPromocao(Guid id, PromocaoRequest request);
    Task RemoverPromocao(Guid id);
}
