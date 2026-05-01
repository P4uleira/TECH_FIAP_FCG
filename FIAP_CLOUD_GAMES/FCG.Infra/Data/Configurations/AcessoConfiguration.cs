using FCG.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infra.Data.Configurations;

public class AcessoConfiguration : IEntityTypeConfiguration<Acesso>
{
    public void Configure(EntityTypeBuilder<Acesso> builder)
    {
        builder.ToTable("Acessos");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.AcessoNome)
            .HasMaxLength(50);

        builder.Property(r => r.AcessoDescricao)
            .HasMaxLength(100);

        builder.HasMany(r => r.Usuarios)
            .WithOne(u => u.Acesso)
            .HasForeignKey(u => u.AcessoId);

        builder.HasData(
            new Acesso { Id = Guid.Parse("0c6cd9e6-f3ea-44a0-9c4d-ecd7906089de"), AcessoNome = "Usuario", AcessoDescricao = "Acesso padrão para usuários" },
            new Acesso { Id = Guid.Parse("8d87caaf-6345-4865-9057-45a8b6b5d882"), AcessoNome = "Administrador", AcessoDescricao = "Acesso completo para administradores" }
        );

    }
}
