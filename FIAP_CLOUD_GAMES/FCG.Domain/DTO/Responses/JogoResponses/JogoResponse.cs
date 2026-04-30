namespace FCG.Domain.DTO.Responses.JogoResponses;

public class JogoResponse
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public DateTime DataCriacao { get; set; }
}
