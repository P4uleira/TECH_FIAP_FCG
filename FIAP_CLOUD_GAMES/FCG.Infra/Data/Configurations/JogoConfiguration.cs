using FCG.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infra.Data.Configurations;

public class JogoConfiguration : IEntityTypeConfiguration<Jogo>
{
    public void Configure(EntityTypeBuilder<Jogo> builder)
    {
        builder.ToTable("Jogos");

        builder.HasKey(j => j.Id);

        builder.Property(j => j.Titulo)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(j => j.Descricao)
            .IsRequired()
            .HasMaxLength(700);

        builder.Property(j => j.Preco)
            .HasPrecision(10, 2);

        builder.HasMany(j => j.Biblioteca)
            .WithOne(uj => uj.Jogo)
            .HasForeignKey(uj => uj.JogoId);
    }
}
