namespace FCG.Domain.Entity;

public class Jogo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; } 
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public ICollection<UsuarioJogo> UsuarioJogos { get; set; } = new List<UsuarioJogo>();
}
