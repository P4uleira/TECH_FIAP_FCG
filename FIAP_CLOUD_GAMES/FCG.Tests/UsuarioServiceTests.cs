using FCG.Domain.Entity;
using FCG.Domain.Exceptions;
using FCG.Domain.Services;
using FluentAssertions;
using Xunit;

namespace FCG.Tests.Domain;

public class UsuarioServiceTests
{
    private readonly UsuarioService _service;

    public UsuarioServiceTests()
    {
        _service = new UsuarioService();
    }

    // ─── Helpers ────────────────────────────────────────────────────────────

    private static Usuario UsuarioValido() => new()
    {
        Nome = "Teste Fiap",
        Email = "teste@fiap.com",
        Senha = "Senha@123"
    };

    // ════════════════════════════════════════════════════════════════════════
    // VALIDAR CRIAÇÃO
    // ════════════════════════════════════════════════════════════════════════

    [Fact(DisplayName = "Given usuário válido, When ValidarCriacao, Then não lança exceção")]
    public async Task ValidarCriacao_UsuarioValido_NaoLancaExcecao()
    {
        // Given
        var usuario = UsuarioValido();

        // When
        var act = async () => await _service.ValidarCriacao(usuario);

        // Then
        await act.Should().NotThrowAsync();
    }

    // ════════════════════════════════════════════════════════════════════════
    // VALIDAR NOME
    // ════════════════════════════════════════════════════════════════════════

    [Fact(DisplayName = "Given nome vazio, When ValidarCriacao, Then lança DomainException")]
    public async Task ValidarCriacao_NomeVazio_LancaDomainException()
    {
        // Given
        var usuario = UsuarioValido();
        usuario.Nome = string.Empty;

        // When
        var act = async () => await _service.ValidarCriacao(usuario);

        // Then
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("O nome do usuário é obrigatório.");
    }

    [Fact(DisplayName = "Given nome com mais de 100 caracteres, When ValidarCriacao, Then lança DomainException")]
    public async Task ValidarCriacao_NomeMuitoLongo_LancaDomainException()
    {
        // Given
        var usuario = UsuarioValido();
        usuario.Nome = new string('A', 101);

        // When
        var act = async () => await _service.ValidarCriacao(usuario);

        // Then
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("O nome do usuário não pode ultrapassar 100 caracteres.");
    }

    // ════════════════════════════════════════════════════════════════════════
    // VALIDAR E-MAIL
    // ════════════════════════════════════════════════════════════════════════

    [Fact(DisplayName = "Given e-mail vazio, When ValidaEmail, Then lança DomainException")]
    public async Task ValidaEmail_EmailVazio_LancaDomainException()
    {
        // Given
        var usuario = UsuarioValido();
        usuario.Email = string.Empty;

        // When
        var act = async () => await _service.ValidaEmail(usuario);

        // Then
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("O e-mail do usuário é obrigatório.");
    }

    [Fact(DisplayName = "Given e-mail sem @, When ValidaEmail, Then lança DomainException")]
    public async Task ValidaEmail_EmailSemArroba_LancaDomainException()
    {
        // Given
        var usuario = UsuarioValido();
        usuario.Email = "emailsemarroba.com";

        // When
        var act = async () => await _service.ValidaEmail(usuario);

        // Then
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("O e-mail informado é inválido.");
    }

    [Fact(DisplayName = "Given e-mail sem domínio, When ValidaEmail, Then lança DomainException")]
    public async Task ValidaEmail_EmailSemDominio_LancaDomainException()
    {
        // Given
        var usuario = UsuarioValido();
        usuario.Email = "email@";

        // When
        var act = async () => await _service.ValidaEmail(usuario);

        // Then
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("O e-mail informado é inválido.");
    }

    [Fact(DisplayName = "Given e-mail válido, When ValidaEmail, Then não lança exceção")]
    public async Task ValidaEmail_EmailValido_NaoLancaExcecao()
    {
        // Given
        var usuario = UsuarioValido();

        // When
        var act = async () => await _service.ValidaEmail(usuario);

        // Then
        await act.Should().NotThrowAsync();
    }

    [Fact(DisplayName = "Given e-mail com mais de 100 caracteres, When ValidaEmail, Then lança DomainException")]
    public async Task ValidaEmail_EmailMuitoLongo_LancaDomainException()
    {
        // Given
        var usuario = UsuarioValido();
        usuario.Email = new string('a', 91) + "@email.com";

        // When
        var act = async () => await _service.ValidaEmail(usuario);

        // Then
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("O e-mail do usuário não pode ultrapassar 100 caracteres.");
    }

    // ════════════════════════════════════════════════════════════════════════
    // VALIDAR SENHA
    // ════════════════════════════════════════════════════════════════════════

    [Fact(DisplayName = "Given senha vazia, When ValidaSenhaForte, Then lança DomainException")]
    public async Task ValidaSenhaForte_SenhaVazia_LancaDomainException()
    {
        // Given
        var usuario = UsuarioValido();
        usuario.Senha = string.Empty;

        // When
        var act = async () => await _service.ValidaSenhaForte(usuario);

        // Then
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("A senha do usuário é obrigatória.");
    }

    [Fact(DisplayName = "Given senha com menos de 8 caracteres, When ValidaSenhaForte, Then lança DomainException")]
    public async Task ValidaSenhaForte_SenhaCurta_LancaDomainException()
    {
        // Given
        var usuario = UsuarioValido();
        usuario.Senha = "Ab@1";

        // When
        var act = async () => await _service.ValidaSenhaForte(usuario);

        // Then
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("A senha deve ter no mínimo 8 caracteres.");
    }

    [Fact(DisplayName = "Given senha sem letras, When ValidaSenhaForte, Then lança DomainException")]
    public async Task ValidaSenhaForte_SenhaSemLetras_LancaDomainException()
    {
        // Given
        var usuario = UsuarioValido();
        usuario.Senha = "12345678@";

        // When
        var act = async () => await _service.ValidaSenhaForte(usuario);

        // Then
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("A senha deve conter pelo menos uma letra.");
    }

    [Fact(DisplayName = "Given senha sem números, When ValidaSenhaForte, Then lança DomainException")]
    public async Task ValidaSenhaForte_SenhaSemNumeros_LancaDomainException()
    {
        // Given
        var usuario = UsuarioValido();
        usuario.Senha = "Senha@abc";

        // When
        var act = async () => await _service.ValidaSenhaForte(usuario);

        // Then
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("A senha deve conter pelo menos um número.");
    }

    [Fact(DisplayName = "Given senha sem caractere especial, When ValidaSenhaForte, Then lança DomainException")]
    public async Task ValidaSenhaForte_SenhaSemCaractereEspecial_LancaDomainException()
    {
        // Given
        var usuario = UsuarioValido();
        usuario.Senha = "Senha1234";

        // When
        var act = async () => await _service.ValidaSenhaForte(usuario);

        // Then
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("A senha deve conter pelo menos um caractere especial.");
    }

    [Fact(DisplayName = "Given senha forte válida, When ValidaSenhaForte, Then não lança exceção")]
    public async Task ValidaSenhaForte_SenhaValida_NaoLancaExcecao()
    {
        // Given
        var usuario = UsuarioValido();

        // When
        var act = async () => await _service.ValidaSenhaForte(usuario);

        // Then
        await act.Should().NotThrowAsync();
    }

    // ════════════════════════════════════════════════════════════════════════
    // TEORIA — múltiplos casos com InlineData
    // ════════════════════════════════════════════════════════════════════════

    [Theory(DisplayName = "Given e-mails inválidos, When ValidaEmail, Then lança DomainException")]
    [InlineData("semArroba")]
    [InlineData("sem@dominio")]
    [InlineData("@semlocal.com")]
    [InlineData("")]
    public async Task ValidaEmail_EmailsInvalidos_LancaDomainException(string emailInvalido)
    {
        // Given
        var usuario = UsuarioValido();
        usuario.Email = emailInvalido;

        // When
        var act = async () => await _service.ValidaEmail(usuario);

        // Then
        await act.Should().ThrowAsync<DomainException>();
    }

    [Theory(DisplayName = "Given senhas fracas, When ValidaSenhaForte, Then lança DomainException")]
    [InlineData("curta")]          // menos de 8 chars
    [InlineData("semNumero@A")]    // sem número
    [InlineData("12345678@")]      // sem letra
    [InlineData("SemEspecial1")]   // sem caractere especial
    public async Task ValidaSenhaForte_SenhasFracas_LancaDomainException(string senhaFraca)
    {
        // Given
        var usuario = UsuarioValido();
        usuario.Senha = senhaFraca;

        // When
        var act = async () => await _service.ValidaSenhaForte(usuario);

        // Then
        await act.Should().ThrowAsync<DomainException>();
    }
}