using AutoMapper;
using FCG.Application.Interfaces;
using FCG.Domain.DTO.Requests.Usuario;
using FCG.Domain.DTO.Requests.UsuarioRequests;
using FCG.Domain.DTO.Responses.UsuarioResponses;
using FCG.Domain.Entity;
using FCG.Domain.Interfaces.Repositories;
using FCG.Domain.Interfaces.Services;
using FCG.Domain.Services;
using Microsoft.Extensions.Logging;

namespace FCG.Application.Handlers;

public class UsuarioHandler : IUsuarioHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUsuarioService _usuarioService;
    private readonly IAuthService _authService;
    private readonly ILogger<UsuarioHandler> _logger;
    private readonly IMapper _mapper;

    public UsuarioHandler(IUsuarioRepository usuarioRepository, IUsuarioService usuarioService, IAuthService authService, ILogger<UsuarioHandler> logger, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _usuarioService = usuarioService;
        _authService = authService;
        _logger = logger;
        _mapper = mapper;
        
    }

    public async Task Atualizar(AtualizarUsuarioRequest request)
    {
        _logger.LogInformation("Atualizando usuário {Id}.", request.Id);

        var usuarioExistente = await _usuarioRepository.BuscarPorId(request.Id);

        if (usuarioExistente is null)
            throw new KeyNotFoundException($"Usuário com ID {request.Id} não encontrado.");

        if (!string.IsNullOrWhiteSpace(request.Nome))
            usuarioExistente.Nome = request.Nome;

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            usuarioExistente.Email = request.Email;
            await _usuarioService.ValidaEmail(usuarioExistente);
        }

        if (!string.IsNullOrWhiteSpace(request.Senha))
        {
            usuarioExistente.Senha = request.Senha;
            await _usuarioService.ValidaSenhaForte(usuarioExistente);
            usuarioExistente.Senha = _authService.HashSenha(request.Senha);
        }

        if (request.AcessoId.HasValue)
            usuarioExistente.AcessoId = request.AcessoId.Value;

        await _usuarioRepository.Atualizar(usuarioExistente);

        _logger.LogInformation("Usuário {Id} atualizado com sucesso.", request.Id);
    }

    public async Task<UsuarioResponse?> BuscarPorId(Guid id)
    {
        _logger.LogInformation("Buscando usuário {Id}.", id);

        var usuario = await _usuarioRepository.BuscarPorId(id);

        if (usuario is null)
        {
            _logger.LogWarning("Usuário {Id} não encontrado.", id);
            return null;
        }

        _logger.LogInformation("Usuário {Id} encontrado com sucesso.", id);
        return _mapper.Map<UsuarioResponse>(usuario);
    }

    public async Task<IEnumerable<UsuarioResponse>> BuscarTodos()
    {
        _logger.LogInformation("Buscando todos os usuários.");

        var usuarios = await _usuarioRepository.BuscarTodos();

        _logger.LogInformation("{Total} usuário(s) encontrado(s).", usuarios.Count());
        return _mapper.Map<IEnumerable<UsuarioResponse>>(usuarios);
    }

    public async Task Criar(CriarUsuarioRequest request)
    {
        _logger.LogInformation("Criando usuário {Nome}.", request.Nome);

        var usuario = _mapper.Map<Usuario>(request);

        await _usuarioService.ValidaEmail(usuario);
        await _usuarioService.ValidaSenhaForte(usuario);

        usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

        await _usuarioRepository.Criar(usuario);

        _logger.LogInformation("Usuário {Nome} criado com sucesso.", request.Nome);
    }

    public async Task Deletar(Guid id)
    {
        _logger.LogInformation("Deletando usuário {Id}.", id);

        await _usuarioRepository.Deletar(id);
        _logger.LogInformation("Usuário {Id} deletado com sucesso.", id);
    }
}
