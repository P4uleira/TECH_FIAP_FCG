using FCG.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infra.Data.Configurations;

public class BibliotecaConfiguration : IEntityTypeConfiguration<Biblioteca>
{
    public void Configure(EntityTypeBuilder<Biblioteca> builder)
    {
        builder.ToTable("Biblioteca");

        builder.HasKey(uj => new { uj.UsuarioId, uj.JogoId });

        builder.HasOne(uj => uj.Usuario)
            .WithMany(u => u.Biblioteca)
            .HasForeignKey(uj => uj.UsuarioId);

        builder.HasOne(uj => uj.Jogo)
            .WithMany(j => j.Biblioteca)
            .HasForeignKey(uj => uj.JogoId);

        builder.Property(uj => uj.DataCompra)
            .IsRequired();
    }
}