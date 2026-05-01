namespace FCG.Domain.Entity;

public class Acesso
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string AcessoNome { get; set; } = string.Empty;
    public string AcessoDescricao { get; set; } = string.Empty;
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
