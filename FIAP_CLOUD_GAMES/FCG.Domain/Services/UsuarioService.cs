using FCG.Domain.Entity;
using FCG.Domain.Exceptions;
using FCG.Domain.Interfaces.Repositories;
using FCG.Domain.Interfaces.Services;
using System.Text.RegularExpressions;

namespace FCG.Domain.Services;

public class UsuarioService : IUsuarioService
{
    public async Task ValidaEmail(Usuario usuario)
    {
        ValidarCampos(usuario);

        var emailValido = Regex.IsMatch(
            usuario.Email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase
        );

        if (!emailValido)
            throw new DomainException("O e-mail informado é inválido.");

    }

    public async Task ValidaSenhaForte(Usuario usuario)
    {
        ValidarCampos(usuario);

        var caracteresEspeciais = @"!@#$%^&*()_+-=[]{}|;':"",./<>?";
        if (!usuario.Senha.Any(c => caracteresEspeciais.Contains(c)))
            throw new DomainException("A senha deve conter pelo menos um caractere especial.");
    }

    private static void ValidarCampos(Usuario usuario)
    {
        if (string.IsNullOrWhiteSpace(usuario.Nome))
            throw new DomainException("O nome do usuário é obrigatório.");

        if (usuario.Nome.Length > 100)
            throw new DomainException("O nome do usuário não pode ultrapassar 100 caracteres.");

        if (string.IsNullOrWhiteSpace(usuario.Email))
            throw new DomainException("O e-mail do usuário é obrigatório.");

        if (usuario.Email.Length > 100)
            throw new DomainException("O e-mail do usuário não pode ultrapassar 100 caracteres.");

        if (string.IsNullOrWhiteSpace(usuario.Senha))
            throw new DomainException("A senha do usuário é obrigatória.");

        if (usuario.Senha.Length < 8)
            throw new DomainException("A senha deve ter no mínimo 8 caracteres.");

        if (!usuario.Senha.Any(char.IsLetter))
            throw new DomainException("A senha deve conter pelo menos uma letra.");

        if (!usuario.Senha.Any(char.IsDigit))
            throw new DomainException("A senha deve conter pelo menos um número.");
    }
}