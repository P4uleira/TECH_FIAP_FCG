using AutoMapper;
using FCG.Domain.DTO.Requests.Usuario;
using FCG.Domain.DTO.Requests.UsuarioRequests;
using FCG.Domain.DTO.Responses.UsuarioResponses;
using FCG.Domain.Entity;
using FCG.Domain.Interfaces.Repositories;
using FCG.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FCG.Domain.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<UsuarioService> _logger;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository, ILogger<UsuarioService> logger, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Atualizar(AtualizarUsuarioRequest request)
    {
        try
        {
            _logger.LogInformation("Atualizando usuário {Id}.", request.Id);
            var usuario = _mapper.Map<Usuario>(request);
            await _usuarioRepository.Atualizar(usuario);
            _logger.LogInformation("Usuário {Id} atualizado com sucesso.", request.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar usuário {Id}.", request.Id);
            throw;
        }
    }

    public async Task<UsuarioResponse?> BuscarPorId(Guid guid)
    {
        try
        {
            _logger.LogInformation("Buscando usuário {Id}.", guid);
            var usuario = await _usuarioRepository.BuscarPorId(guid);

            if (usuario is null)
            {
                _logger.LogWarning("Usuário {Id} não encontrado.", guid);
                return null;
            }

            _logger.LogInformation("Usuário {Id} encontrado com sucesso.", guid);
            return _mapper.Map<UsuarioResponse>(usuario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar usuário {Id}.", guid);
            throw;
        }
    }

    public async Task<IEnumerable<UsuarioResponse>> BuscarTodos()
    {
        try
        {
            _logger.LogInformation("Buscando todos os usuários.");
            var usuarios = await _usuarioRepository.BuscarTodos();
            _logger.LogInformation("{Total} usuário(s) encontrado(s).", usuarios.Count());
            return _mapper.Map<IEnumerable<UsuarioResponse>>(usuarios);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar todos os usuários.");
            throw;
        }
    }

    public async Task Criar(CriarUsuarioRequest request)
    {
        try
        {
            _logger.LogInformation("Criando usuário {Email}.", request.Email);
            var usuario = _mapper.Map<Usuario>(request);
            await _usuarioRepository.Criar(usuario);
            _logger.LogInformation("Usuário {Email} criado com sucesso.", request.Email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar usuário {Email}.", request.Email);
            throw;
        }
    }

    public async Task Deletar(Guid guid)
    {
        try
        {
            _logger.LogInformation("Deletando usuário {Id}.", guid);
            await _usuarioRepository.Deletar(guid);
            _logger.LogInformation("Usuário {Id} deletado com sucesso.", guid);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar usuário {Id}.", guid);
            throw;
        }
    }
}
