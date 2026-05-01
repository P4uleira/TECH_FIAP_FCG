using FCG.Application.Interfaces;
using FCG.Domain.DTO.Requests.LoginRequests;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthHandler _authHandler;

    public AuthController(IAuthHandler authHandler)
    {
        _authHandler = authHandler;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authHandler.LoginAsync(request);
        return Ok(response);
    }
}
