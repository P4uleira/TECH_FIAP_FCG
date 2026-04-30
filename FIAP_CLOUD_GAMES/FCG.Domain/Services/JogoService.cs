using AutoMapper;
using FCG.Domain.DTO.Requests.Jogo;
using FCG.Domain.DTO.Requests.JogoRequests;
using FCG.Domain.DTO.Responses.JogoResponses;
using FCG.Domain.Entity;
using FCG.Domain.Interfaces.Repositories;
using FCG.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FCG.Domain.Services;

public class JogoService : IJogoService
{
    private readonly IJogoRepository _jogoRepository;
    private readonly ILogger<JogoService> _logger;
    private readonly IMapper _mapper;

    public JogoService(IJogoRepository jogoRepository, ILogger<JogoService> logger, IMapper mapper)
    {
        _jogoRepository = jogoRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Atualizar(AtualizarJogoRequest request)
    {
        try
        {
            _logger.LogInformation("Atualizando jogo {Id}.", request.Id);
            var jogo = _mapper.Map<Jogo>(request);
            await _jogoRepository.Atualizar(jogo);
            _logger.LogInformation("Jogo {Id} atualizado com sucesso.", request.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar jogo {Id}.", request.Id);
            throw;
        }
    }

    public async Task<JogoResponse?> BuscarPorId(Guid guid)
    {
        try
        {
            _logger.LogInformation("Buscando jogo {Id}.", guid);
            var jogo = await _jogoRepository.BuscarPorId(guid);

            if (jogo is null)
            {
                _logger.LogWarning("Jogo {Id} não encontrado.", guid);
                return null;
            }

            _logger.LogInformation("Jogo {Id} encontrado com sucesso.", guid);
            return _mapper.Map<JogoResponse>(jogo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar jogo {Id}.", guid);
            throw;
        }
    }

    public async Task<IEnumerable<JogoResponse>> BuscarTodos()
    {
        try
        {
            _logger.LogInformation("Buscando todos os jogos.");
            var jogos = await _jogoRepository.BuscarTodos();
            _logger.LogInformation("{Total} jogo(s) encontrado(s).", jogos.Count());
            return _mapper.Map<IEnumerable<JogoResponse>>(jogos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar todos os jogos.");
            throw;
        }
    }

    public async Task Criar(CriarJogoRequest request)
    {
        try
        {
            _logger.LogInformation("Criando jogo {Titulo}.", request.Titulo);
            var jogo = _mapper.Map<Jogo>(request);
            await _jogoRepository.Criar(jogo);
            _logger.LogInformation("Jogo {Titulo} criado com sucesso.", request.Titulo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar jogo {Titulo}.", request.Titulo);
            throw;
        }
    }

    public async Task Deletar(Guid guid)
    {
        try
        {
            _logger.LogInformation("Deletando jogo {Id}.", guid);
            await _jogoRepository.Deletar(guid);
            _logger.LogInformation("Jogo {Id} deletado com sucesso.", guid);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar jogo {Id}.", guid);
            throw;
        }
    }
}
