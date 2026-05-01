namespace FCG.Domain.DTO.Responses.BibliotecaResponses;

public class BibliotecaResponse
{
    public Guid JogoId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public DateTime DataAquisicao { get; set; }
}
