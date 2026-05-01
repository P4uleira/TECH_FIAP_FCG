using FCG.Domain.Entity;

namespace FCG.Domain.Interfaces.Services;

public interface IAuthService
{
    string GerarToken(Usuario usuario);
    string HashSenha(string senha);
    bool VerificarSenha(string senha, string hash);
}
