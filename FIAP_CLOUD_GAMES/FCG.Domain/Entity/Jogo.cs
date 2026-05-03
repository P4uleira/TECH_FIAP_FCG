namespace FCG.Domain.Entity;

public class Jogo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public decimal? PrecoPromocional { get; set; }
    public DateTime? PromocaoExpiracao { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public ICollection<Biblioteca> Biblioteca { get; set; } = new List<Biblioteca>();

    public decimal PrecoAtual => PrecoPromocional.HasValue &&
                                 (PromocaoExpiracao == null || PromocaoExpiracao > DateTime.Now)
                                 ? PrecoPromocional.Value
                                 : Preco;

    public bool EmPromocao => PrecoPromocional.HasValue &&
                              (PromocaoExpiracao == null || PromocaoExpiracao > DateTime.Now);
}
