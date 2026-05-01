using FCG.Application.Interfaces;
using FCG.Domain.DTO.Requests.LoginRequests;
using FCG.Domain.DTO.Responses.LoginResponses;
using FCG.Domain.Exceptions;
using FCG.Domain.Interfaces.Repositories;
using FCG.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FCG.Application.Handlers
{
    public class AuthHandler : IAuthHandler
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthService _authService;
        private readonly ILogger<AuthHandler> _logger;

        public AuthHandler(
            IUsuarioRepository usuarioRepository,
            IAuthService authService,
            ILogger<AuthHandler> logger)
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;
            _logger = logger;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            _logger.LogInformation("Tentativa de login para {Email}.", request.Email);

            var usuario = await _usuarioRepository.BuscarPorEmail(request.Email);

            if (usuario is null || !_authService.VerificarSenha(request.Senha, usuario.Senha))
                throw new DomainException("E-mail ou senha inválidos.");

            var token = _authService.GerarToken(usuario);

            _logger.LogInformation("Login realizado com sucesso para {Email}.", request.Email);

            return new LoginResponse
            {
                Token = token,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Acesso = usuario.Acesso.AcessoNome,
                Expiracao = DateTime.UtcNow.AddHours(8)
            };
        }
    }
}
