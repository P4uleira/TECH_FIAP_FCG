namespace FCG.Domain.DTO.Requests.UsuarioRequests;

public class AtualizarUsuarioRequest
{
    public Guid Id { get; set; }
    public string? Nome { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Senha { get; set; }
    public Guid? AcessoId { get; set; }
}
