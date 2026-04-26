using FCG.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infra.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Senha)
            .IsRequired();


        builder.HasOne(u => u.Acesso)
            .WithMany(r => r.Usuarios)
            .HasForeignKey(u => u.AcessoId);


        builder.HasMany(u => u.UsuarioJogos)
            .WithOne(uj => uj.Usuario)
            .HasForeignKey(uj => uj.UsuarioId);
    }
}
