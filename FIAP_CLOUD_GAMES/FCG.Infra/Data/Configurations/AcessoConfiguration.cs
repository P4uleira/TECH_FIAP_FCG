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

        builder.HasMany(r => r.Usuarios)
            .WithOne(u => u.Acesso)
            .HasForeignKey(u => u.AcessoId);
    }
}
