using FCG.Domain.DTO.Responses.BibliotecaResponses;

namespace FCG.Application.Interfaces;

public interface IBibliotecaHandler
{
    Task<IEnumerable<BibliotecaResponse>> BuscarPorUsuario(Guid usuarioId);
    Task AdicionarJogo(Guid usuarioId, Guid jogoId);
    Task RemoverJogo(Guid usuarioId, Guid jogoId);
}
