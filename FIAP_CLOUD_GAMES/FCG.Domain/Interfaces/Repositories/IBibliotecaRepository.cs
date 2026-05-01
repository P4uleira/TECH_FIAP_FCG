using FCG.Domain.Entity;

namespace FCG.Domain.Interfaces.Repositories;

public interface IBibliotecaRepository
{
    Task<IEnumerable<Biblioteca>> BuscarPorUsuario(Guid usuarioId);
    Task<Biblioteca?> BuscarPorUsuarioEJogo(Guid usuarioId, Guid jogoId);
    Task Adicionar(Biblioteca biblioteca);
    Task Remover(Biblioteca biblioteca);
}

