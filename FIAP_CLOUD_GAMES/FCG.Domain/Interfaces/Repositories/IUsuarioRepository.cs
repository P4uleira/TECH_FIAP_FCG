using FCG.Domain.Entity;

namespace FCG.Domain.Interfaces.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario?> BuscarPorEmail(string email);
    Task<bool> ExisteEmail(string email);
}
