using AutoMapper;
using FCG.Application.Interfaces;
using FCG.Domain.DTO.Responses.BibliotecaResponses;
using FCG.Domain.Entity;
using FCG.Domain.Exceptions;
using FCG.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace FCG.Application.Handlers;

public class BibliotecaHandler : IBibliotecaHandler
{
    private readonly IBibliotecaRepository _bibliotecaRepository;
    private readonly IJogoRepository _jogoRepository;
    private readonly ILogger<BibliotecaHandler> _logger;
    private readonly IMapper _mapper;

    public BibliotecaHandler(
        IBibliotecaRepository bibliotecaRepository,
        IJogoRepository jogoRepository,
        ILogger<BibliotecaHandler> logger,
        IMapper mapper)
    {
        _bibliotecaRepository = bibliotecaRepository;
        _jogoRepository = jogoRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BibliotecaResponse>> BuscarPorUsuario(Guid usuarioId)
    {
        _logger.LogInformation("Buscando biblioteca do usuário {UsuarioId}.", usuarioId);

        var itens = await _bibliotecaRepository.BuscarPorUsuario(usuarioId);

        return _mapper.Map<IEnumerable<BibliotecaResponse>>(itens);
    }

    public async Task AdicionarJogo(Guid usuarioId, Guid jogoId)
    {
        _logger.LogInformation("Adicionando jogo {JogoId} à biblioteca do usuário {UsuarioId}.", jogoId, usuarioId);

        var jogoExistente = await _jogoRepository.BuscarPorId(jogoId);
        if (jogoExistente is null)
            throw new KeyNotFoundException($"Jogo com ID {jogoId} não encontrado.");

        var jaAdicionado = await _bibliotecaRepository.BuscarPorUsuarioEJogo(usuarioId, jogoId);
        if (jaAdicionado is not null)
            throw new DomainException("Este jogo já está na sua biblioteca.");

        var biblioteca = new Biblioteca
        {
            UsuarioId = usuarioId,
            JogoId = jogoId,
            DataCompra = DateTime.UtcNow
        };

        await _bibliotecaRepository.Adicionar(biblioteca);

        _logger.LogInformation("Jogo {JogoId} adicionado com sucesso.", jogoId);
    }

    public async Task RemoverJogo(Guid usuarioId, Guid jogoId)
    {
        _logger.LogInformation("Removendo jogo {JogoId} da biblioteca do usuário {UsuarioId}.", jogoId, usuarioId);

        var item = await _bibliotecaRepository.BuscarPorUsuarioEJogo(usuarioId, jogoId);
        if (item is null)
            throw new KeyNotFoundException("Jogo não encontrado na biblioteca.");

        await _bibliotecaRepository.Remover(item);

        _logger.LogInformation("Jogo {JogoId} removido com sucesso.", jogoId);
    }
}
