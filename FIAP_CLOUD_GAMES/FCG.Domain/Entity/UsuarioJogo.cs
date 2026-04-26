namespace FCG.Domain.Entity;

public class UsuarioJogo
{
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public Guid JogoId { get; set; }
    public Jogo Jogo { get; set; } = null!;
    public DateTime DataCompra { get; set; } = DateTime.UtcNow;
}   
