namespace FCG.Domain.DTO.Requests.Jogo;

public class AtualizarJogoRequest
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
}
