using FCG.Domain.Entity;

namespace FCG.Domain.Interfaces.Services;

public interface IJogoService
{
    Task ValidarCriacao(Jogo jogo);
    Task ValidarAtualizacao(Jogo jogo);
}