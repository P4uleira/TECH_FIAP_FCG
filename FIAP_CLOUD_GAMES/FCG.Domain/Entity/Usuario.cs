namespace FCG.Domain.Entity;

public class Usuario
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public Guid AcessoId { get; set; }
    public Acesso Acesso { get; set; } = null!;

    public ICollection<UsuarioJogo> UsuarioJogos { get; set; } = new List<UsuarioJogo>();

}
