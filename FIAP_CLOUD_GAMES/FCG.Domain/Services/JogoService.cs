using FCG.Domain.Entity;
using FCG.Domain.Exceptions;
using FCG.Domain.Interfaces.Repositories;
using FCG.Domain.Interfaces.Services;

namespace FCG.Domain.Services;

public class JogoService : IJogoService
{
    private readonly IJogoRepository _jogoRepository;
    public JogoService(IJogoRepository jogoRepository)
    {
        _jogoRepository = jogoRepository;
    }

    public async Task ValidarCriacao(Jogo jogo)
    {
        ValidarCampos(jogo);

        var tituloExistente = await _jogoRepository.ExisteTitulo(jogo.Titulo);
        if (tituloExistente)
            throw new DomainException($"Já existe um jogo cadastrado com o título '{jogo.Titulo}'.");
    }

    public async Task ValidarAtualizacao(Jogo jogo)
    {
        ValidarCampos(jogo);

        var tituloExistente = await _jogoRepository.ExisteTituloEmOutroJogo(jogo.Id, jogo.Titulo);
        if (tituloExistente)
            throw new DomainException($"Já existe outro jogo cadastrado com o título '{jogo.Titulo}'.");
    }

    private static void ValidarCampos(Jogo jogo)
    {
        if (string.IsNullOrWhiteSpace(jogo.Titulo))
            throw new DomainException("O título do jogo é obrigatório.");

        if (jogo.Titulo.Length > 200)
            throw new DomainException("O título do jogo não pode ultrapassar 200 caracteres.");

        if (string.IsNullOrWhiteSpace(jogo.Descricao))
            throw new DomainException("A descrição do jogo é obrigatória.");

        if (jogo.Preco < 0)
            throw new DomainException("O preço do jogo não pode ser negativo.");

        if (jogo.DataCriacao > DateTime.UtcNow)
            throw new DomainException("A data de criação do jogo não pode ser futura.");
    }
}
