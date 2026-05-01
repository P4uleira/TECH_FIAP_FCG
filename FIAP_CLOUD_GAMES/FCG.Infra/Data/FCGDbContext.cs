namespace FCG.Infra.Data;

using FCG.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class FCGDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public FCGDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FCGDbContext).Assembly);
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Jogo> Jogos => Set<Jogo>();
    public DbSet<Acesso> Acessos => Set<Acesso>();
    public DbSet<Biblioteca> UsuarioJogos => Set<Biblioteca>();

}
