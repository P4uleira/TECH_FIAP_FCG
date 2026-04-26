using FCG.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infra.Data.Configurations;

public class UsuarioJogoConfiguration : IEntityTypeConfiguration<UsuarioJogo>
{
    public void Configure(EntityTypeBuilder<UsuarioJogo> builder)
    {
        builder.ToTable("UsuarioJogos");

        builder.HasKey(uj => new { uj.UsuarioId, uj.JogoId });

        builder.HasOne(uj => uj.Usuario)
            .WithMany(u => u.UsuarioJogos)
            .HasForeignKey(uj => uj.UsuarioId);

        builder.HasOne(uj => uj.Jogo)
            .WithMany(j => j.UsuarioJogos)
            .HasForeignKey(uj => uj.JogoId);

        builder.Property(uj => uj.DataCompra)
            .IsRequired();
    }
}