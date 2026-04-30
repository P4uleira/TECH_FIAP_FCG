namespace FCG.Domain.DTO.Responses.UsuarioResponses;

public class UsuarioResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid AcessoId { get; set; }
}
