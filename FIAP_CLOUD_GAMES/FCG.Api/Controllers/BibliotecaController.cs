using FCG.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FCG.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BibliotecaController : ControllerBase
{
    private readonly IBibliotecaHandler _handler;

    public BibliotecaController(IBibliotecaHandler handler)
    {
        _handler = handler;
    }

    [HttpGet]
    public async Task<IActionResult> BuscarMinhaBiblioteca()
    {
        var usuarioId = ObterUsuarioId();
        var response = await _handler.BuscarPorUsuario(usuarioId);
        return Ok(response);
    }

    [HttpPost("{jogoId:guid}")]
    public async Task<IActionResult> AdicionarJogo(Guid jogoId)
    {
        var usuarioId = ObterUsuarioId();
        await _handler.AdicionarJogo(usuarioId, jogoId);
        return NoContent();
    }

    [HttpDelete("{jogoId:guid}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> RemoverJogo(Guid jogoId)
    {
        var usuarioId = ObterUsuarioId();
        await _handler.RemoverJogo(usuarioId, jogoId);
        return NoContent();
    }

    private Guid ObterUsuarioId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(claim))
            throw new UnauthorizedAccessException("Usuário não identificado no token.");

        return Guid.Parse(claim);
    }
}