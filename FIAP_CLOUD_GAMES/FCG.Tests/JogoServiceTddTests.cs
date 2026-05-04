using FCG.Domain.Entity;
using FCG.Domain.Exceptions;
using FCG.Domain.Interfaces.Repositories;
using FCG.Domain.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace FCG.Tests.TDD;
public class JogoServiceTddTests
{
    private readonly Mock<IJogoRepository> _repositoryMock;
    private readonly JogoService _service;

    public JogoServiceTddTests()
    {
        _repositoryMock = new Mock<IJogoRepository>();
        _service = new JogoService(_repositoryMock.Object);
    }

    private static Jogo JogoValido() => new()
    {
        Titulo = "The Witcher 3",
        Descricao = "RPG de mundo aberto",
        Preco = 99.90m,
        DataCriacao = DateTime.UtcNow.AddDays(-1)
    };

    // ════════════════════════════════════════════════════════════════════════
    // CICLO 1 — ValidarCampos: Título
    // RED: teste escrito, ValidarCampos não existe → falha
    // GREEN: implementa a validação de título vazio
    // REFACTOR: extrai ValidarCampos como método privado estático
    // ════════════════════════════════════════════════════════════════════════

    [Fact(DisplayName = "[RED→GREEN] Título vazio deve lançar DomainException")]
    public async Task ValidarCriacao_TituloVazio_LancaDomainException()
    {
        var jogo = JogoValido();
        jogo.Titulo = string.Empty;

        var act = async () => await _service.ValidarCriacao(jogo);

        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("O título do jogo é obrigatório.");
    }

    [Fact(DisplayName = "[RED→GREEN] Título acima de 200 caracteres deve lançar DomainException")]
    public async Task ValidarCriacao_TituloMuitoLongo_LancaDomainException()
    {
        var jogo = JogoValido();
        jogo.Titulo = new string('A', 201);

        var act = async () => await _service.ValidarCriacao(jogo);

        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("O título do jogo não pode ultrapassar 200 caracteres.");
    }

    // ════════════════════════════════════════════════════════════════════════
    // CICLO 2 — ValidarCampos: Descrição
    // RED: teste escrito, validação de descrição não existe → falha
    // GREEN: adiciona validação de descrição vazia
    // REFACTOR: sem mudança necessária
    // ════════════════════════════════════════════════════════════════════════

    [Fact(DisplayName = "[RED→GREEN] Descrição vazia deve lançar DomainException")]
    public async Task ValidarCriacao_DescricaoVazia_LancaDomainException()
    {
        var jogo = JogoValido();
        jogo.Descricao = string.Empty;

        var act = async () => await _service.ValidarCriacao(jogo);

        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("A descrição do jogo é obrigatória.");
    }

    // ════════════════════════════════════════════════════════════════════════
    // CICLO 3 — ValidarCampos: Preço
    // RED: teste escrito, validação de preço não existe → falha
    // GREEN: adiciona validação Preco < 0
    // REFACTOR: percebe que zero é válido (jogo gratuito), ajusta regra
    // ════════════════════════════════════════════════════════════════════════

    [Fact(DisplayName = "[RED→GREEN] Preço negativo deve lançar DomainException")]
    public async Task ValidarCriacao_PrecoNegativo_LancaDomainException()
    {
        var jogo = JogoValido();
        jogo.Preco = -1m;

        var act = async () => await _service.ValidarCriacao(jogo);

        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("O preço do jogo não pode ser negativo.");
    }

    [Fact(DisplayName = "[REFACTOR] Preço zero deve ser permitido (jogo gratuito)")]
    public async Task ValidarCriacao_PrecoZero_NaoLancaExcecao()
    {
        var jogo = JogoValido();
        jogo.Preco = 0m;

        _repositoryMock
            .Setup(r => r.ExisteTitulo(jogo.Titulo))
            .ReturnsAsync(false);

        var act = async () => await _service.ValidarCriacao(jogo);

        await act.Should().NotThrowAsync();
    }

    // ════════════════════════════════════════════════════════════════════════
    // CICLO 4 — ValidarCampos: DataCriacao
    // RED: teste escrito, validação de data não existe → falha
    // GREEN: adiciona validação DataCriacao > DateTime.UtcNow
    // REFACTOR: sem mudança necessária
    // ════════════════════════════════════════════════════════════════════════

    [Fact(DisplayName = "[RED→GREEN] Data de criação futura deve lançar DomainException")]
    public async Task ValidarCriacao_DataCriacaoFutura_LancaDomainException()
    {
        var jogo = JogoValido();
        jogo.DataCriacao = DateTime.UtcNow.AddDays(1);

        var act = async () => await _service.ValidarCriacao(jogo);

        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("A data de criação do jogo não pode ser futura.");
    }

    // ════════════════════════════════════════════════════════════════════════
    // CICLO 5 — ValidarCriacao: Título duplicado
    // RED: teste escrito, chamada ao repository não existe → falha
    // GREEN: adiciona chamada ao _repository.ExisteTitulo
    // REFACTOR: sem mudança necessária
    // ════════════════════════════════════════════════════════════════════════

    [Fact(DisplayName = "[RED→GREEN] Título duplicado deve lançar DomainException")]
    public async Task ValidarCriacao_TituloDuplicado_LancaDomainException()
    {
        var jogo = JogoValido();

        _repositoryMock
            .Setup(r => r.ExisteTitulo(jogo.Titulo))
            .ReturnsAsync(true);

        var act = async () => await _service.ValidarCriacao(jogo);

        await act.Should().ThrowAsync<DomainException>()
            .WithMessage($"Já existe um jogo cadastrado com o título '{jogo.Titulo}'.");
    }

    [Fact(DisplayName = "[GREEN] Jogo válido não deve lançar exceção na criação")]
    public async Task ValidarCriacao_JogoValido_NaoLancaExcecao()
    {
        var jogo = JogoValido();

        _repositoryMock
            .Setup(r => r.ExisteTitulo(jogo.Titulo))
            .ReturnsAsync(false);

        var act = async () => await _service.ValidarCriacao(jogo);

        await act.Should().NotThrowAsync();
    }

    // ════════════════════════════════════════════════════════════════════════
    // CICLO 6 — ValidarAtualizacao: Título duplicado em outro jogo
    // RED: teste escrito, ExisteTituloEmOutroJogo não é chamado → falha
    // GREEN: adiciona chamada ao _repository.ExisteTituloEmOutroJogo
    // REFACTOR: sem mudança necessária
    // ════════════════════════════════════════════════════════════════════════

    [Fact(DisplayName = "[RED→GREEN] Título duplicado em outro jogo deve lançar DomainException na atualização")]
    public async Task ValidarAtualizacao_TituloDuplicadoEmOutroJogo_LancaDomainException()
    {
        var jogo = JogoValido();

        _repositoryMock
            .Setup(r => r.ExisteTituloEmOutroJogo(jogo.Id, jogo.Titulo))
            .ReturnsAsync(true);

        var act = async () => await _service.ValidarAtualizacao(jogo);

        await act.Should().ThrowAsync<DomainException>()
            .WithMessage($"Já existe outro jogo cadastrado com o título '{jogo.Titulo}'.");
    }

    [Fact(DisplayName = "[GREEN] Atualização com título único não deve lançar exceção")]
    public async Task ValidarAtualizacao_TituloUnico_NaoLancaExcecao()
    {
        var jogo = JogoValido();

        _repositoryMock
            .Setup(r => r.ExisteTituloEmOutroJogo(jogo.Id, jogo.Titulo))
            .ReturnsAsync(false);

        var act = async () => await _service.ValidarAtualizacao(jogo);

        await act.Should().NotThrowAsync();
    }

    // ════════════════════════════════════════════════════════════════════════
    // CICLO 7 — ValidarPromocao
    // RED: teste escrito, ValidarPromocao não existe → falha
    // GREEN: implementa as três regras de promoção
    // REFACTOR: sem mudança necessária
    // ════════════════════════════════════════════════════════════════════════

    [Fact(DisplayName = "[RED→GREEN] Preço promocional zero ou negativo deve lançar DomainException")]
    public void ValidarPromocao_PrecoZeroOuNegativo_LancaDomainException()
    {
        var jogo = JogoValido();

        var act = () => _service.ValidarPromocao(jogo, precoPromocional: 0m, expiracao: null);

        act.Should().Throw<DomainException>()
            .WithMessage("O preço promocional deve ser maior que zero.");
    }

    [Fact(DisplayName = "[RED→GREEN] Preço promocional maior ou igual ao original deve lançar DomainException")]
    public void ValidarPromocao_PrecoMaiorOuIgualAoOriginal_LancaDomainException()
    {
        var jogo = JogoValido(); // Preco = 99.90

        var act = () => _service.ValidarPromocao(jogo, precoPromocional: 99.90m, expiracao: null);

        act.Should().Throw<DomainException>()
            .WithMessage("O preço promocional deve ser menor que o preço original.");
    }

    [Fact(DisplayName = "[RED→GREEN] Data de expiração no passado deve lançar DomainException")]
    public void ValidarPromocao_ExpiracaoNoPassado_LancaDomainException()
    {
        var jogo = JogoValido();

        var act = () => _service.ValidarPromocao(jogo, precoPromocional: 49.90m, expiracao: DateTime.UtcNow.AddDays(-1));

        act.Should().Throw<DomainException>()
            .WithMessage("A data de expiração da promoção deve ser futura.");
    }

    [Fact(DisplayName = "[GREEN] Promoção sem expiração deve ser permitida")]
    public void ValidarPromocao_SemExpiracao_NaoLancaExcecao()
    {
        var jogo = JogoValido();

        var act = () => _service.ValidarPromocao(jogo, precoPromocional: 49.90m, expiracao: null);

        act.Should().NotThrow();
    }

    [Fact(DisplayName = "[GREEN] Promoção válida com expiração futura não deve lançar exceção")]
    public void ValidarPromocao_PromocaoValida_NaoLancaExcecao()
    {
        var jogo = JogoValido();

        var act = () => _service.ValidarPromocao(jogo, precoPromocional: 49.90m, expiracao: DateTime.UtcNow.AddDays(7));

        act.Should().NotThrow();
    }

    // ════════════════════════════════════════════════════════════════════════
    // TEORIA — múltiplos cenários inválidos
    // ════════════════════════════════════════════════════════════════════════

    [Theory(DisplayName = "[TDD] Campos inválidos devem lançar DomainException")]
    [InlineData("", "Descrição válida", 10)]        // título vazio
    [InlineData("Título", "", 10)]                  // descrição vazia
    [InlineData("Título", "Descrição válida", -1)]  // preço negativo
    public async Task ValidarCriacao_CamposInvalidos_LancaDomainException(
        string titulo, string descricao, decimal preco)
    {
        var jogo = new Jogo
        {
            Titulo = titulo,
            Descricao = descricao,
            Preco = preco,
            DataCriacao = DateTime.UtcNow.AddDays(-1)
        };

        var act = async () => await _service.ValidarCriacao(jogo);

        await act.Should().ThrowAsync<DomainException>();
    }
}