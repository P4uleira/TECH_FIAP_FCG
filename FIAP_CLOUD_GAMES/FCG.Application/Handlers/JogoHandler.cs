using AutoMapper;
using FCG.Application.Interfaces;
using FCG.Domain.DTO.Requests.Jogo;
using FCG.Domain.DTO.Requests.JogoRequests;
using FCG.Domain.DTO.Responses.JogoResponses;
using FCG.Domain.Entity;
using FCG.Domain.Interfaces.Repositories;
using FCG.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FCG.Application.Handlers;

public class JogoHandler : IJogoHandler
{
    private readonly IJogoRepository _jogoRepository;
    private readonly IJogoService _jogoService;
    private readonly ILogger<JogoHandler> _logger;
    private readonly IMapper _mapper;

    public JogoHandler(IJogoRepository jogoRepository, IJogoService jogoService, ILogger<JogoHandler> logger, IMapper mapper)
    {
        _jogoRepository = jogoRepository;
        _jogoService = jogoService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Atualizar(AtualizarJogoRequest request)
    {
        _logger.LogInformation("Atualizando jogo {Id}.", request.Id);

        var jogoExistente = await _jogoRepository.BuscarPorId(request.Id);

        if (jogoExistente is null)
            throw new KeyNotFoundException($"Jogo com ID {request.Id} não encontrado.");

        _mapper.Map(request, jogoExistente);

        await _jogoService.ValidarAtualizacao(jogoExistente);
        await _jogoRepository.Atualizar(jogoExistente);

        _logger.LogInformation("Jogo {Id} atualizado com sucesso.", request.Id);
    }

    public async Task<JogoResponse?> BuscarPorId(Guid id)
    {
        _logger.LogInformation("Buscando jogo {Id}.", id);

        var jogo = await _jogoRepository.BuscarPorId(id);

        if (jogo is null)
        {
            _logger.LogWarning("Jogo {Id} não encontrado.", id);
            return null;
        }

        _logger.LogInformation("Jogo {Id} encontrado com sucesso.", id);
        return _mapper.Map<JogoResponse>(jogo);
    }

    public async Task<IEnumerable<JogoResponse>> BuscarTodos()
    {
        _logger.LogInformation("Buscando todos os jogos.");

        var jogos = await _jogoRepository.BuscarTodos();

        _logger.LogInformation("{Total} jogo(s) encontrado(s).", jogos.Count());
        return _mapper.Map<IEnumerable<JogoResponse>>(jogos);
    }

    public async Task Criar(CriarJogoRequest request)
    {
        _logger.LogInformation("Criando jogo {Titulo}.", request.Titulo);

        var jogo = _mapper.Map<Jogo>(request);

        await _jogoService.ValidarCriacao(jogo);
        await _jogoRepository.Criar(jogo);

        _logger.LogInformation("Jogo {Titulo} criado com sucesso.", request.Titulo);
    }

    public async Task Deletar(Guid id)
    {
        _logger.LogInformation("Deletando jogo {Id}.", id);

        await _jogoRepository.Deletar(id);
        _logger.LogInformation("Jogo {Id} deletado com sucesso.", id);
    }
}
