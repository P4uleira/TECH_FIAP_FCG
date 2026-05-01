using FCG.Domain.Entity;

namespace FCG.Domain.Interfaces.Services;

public interface IUsuarioService
{
    Task ValidaEmail(Usuario usuario);
    Task ValidaSenhaForte(Usuario usuario);
}
