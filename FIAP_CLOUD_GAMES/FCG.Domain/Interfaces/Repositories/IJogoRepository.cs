using FCG.Domain.Entity;

namespace FCG.Domain.Interfaces.Repositories;

public interface IJogoRepository : IRepository<Jogo>
{
    Task<bool> ExisteTitulo(string titulo);
    Task<bool> ExisteTituloEmOutroJogo(Guid id, string titulo);
}
