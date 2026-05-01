using FCG.Application.Interfaces;
using FCG.Domain.DTO.Requests.Usuario;
using FCG.Domain.DTO.Requests.UsuarioRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioHandler _handler;

    public UsuarioController(IUsuarioHandler handler)
    {
        _handler = handler;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> BuscarPorId(Guid id)
    {
        var response = await _handler.BuscarPorId(id);

        if (response is null)
            return NotFound(new { mensagem = $"Usuário com ID {id} não encontrado." });

        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> BuscarTodos()
    {
        var response = await _handler.BuscarTodos();
        return Ok(response);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Criar([FromBody] CriarUsuarioRequest request)
    {
        await _handler.Criar(request);
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> Atualizar([FromBody] AtualizarUsuarioRequest request)
    {
        await _handler.Atualizar(request);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        await _handler.Deletar(id);
        return NoContent();
    }
}
