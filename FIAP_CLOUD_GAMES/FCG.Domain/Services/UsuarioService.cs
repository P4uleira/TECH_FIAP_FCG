using FCG.Domain.Entity;
using FCG.Domain.Exceptions;
using FCG.Domain.Interfaces.Repositories;
using FCG.Domain.Interfaces.Services;
using System.Text.RegularExpressions;

namespace FCG.Domain.Services;

public class UsuarioService : IUsuarioService
{
    private static readonly string CaracteresEspeciais = @"!@#$%^&*()_+-=[]{}|;':"",./<>?";

    public async Task ValidaEmail(Usuario usuario)
    {
        ValidarEmail(usuario.Email);
    }

    public async Task ValidaSenhaForte(Usuario usuario)
    {
        ValidarSenha(usuario.Senha);
    }

    public async Task ValidarCriacao(Usuario usuario)
    {
        ValidarNome(usuario.Nome);
        ValidarEmail(usuario.Email);
        ValidarSenha(usuario.Senha);
    }

    private static void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("O nome do usuário é obrigatório.");

        if (nome.Length > 100)
            throw new DomainException("O nome do usuário não pode ultrapassar 100 caracteres.");
    }

    private static void ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("O e-mail do usuário é obrigatório.");

        if (email.Length > 100)
            throw new DomainException("O e-mail do usuário não pode ultrapassar 100 caracteres.");

        var emailValido = Regex.IsMatch(
            email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase
        );

        if (!emailValido)
            throw new DomainException("O e-mail informado é inválido.");
    }

    private static void ValidarSenha(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
            throw new DomainException("A senha do usuário é obrigatória.");

        if (senha.Length < 8)
            throw new DomainException("A senha deve ter no mínimo 8 caracteres.");

        if (!senha.Any(char.IsLetter))
            throw new DomainException("A senha deve conter pelo menos uma letra.");

        if (!senha.Any(char.IsDigit))
            throw new DomainException("A senha deve conter pelo menos um número.");

        if (!senha.Any(c => CaracteresEspeciais.Contains(c)))
            throw new DomainException("A senha deve conter pelo menos um caractere especial.");
    }
}