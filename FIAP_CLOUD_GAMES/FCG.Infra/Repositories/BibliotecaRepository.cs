using FCG.Domain.Entity;
using FCG.Domain.Interfaces.Repositories;
using FCG.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Repositories;

public class BibliotecaRepository : IBibliotecaRepository
{
    private readonly FCGDbContext _dbContext;

    public BibliotecaRepository(FCGDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Biblioteca>> BuscarPorUsuario(Guid usuarioId)
    {
        return await _dbContext.Bibliotecas
            .Include(b => b.Jogo)
            .Where(b => b.UsuarioId == usuarioId)
            .OrderBy(b => b.Jogo.Titulo)
            .ToListAsync();
    }

    public async Task<Biblioteca?> BuscarPorUsuarioEJogo(Guid usuarioId, Guid jogoId)
    {
        return await _dbContext.Bibliotecas
            .FirstOrDefaultAsync(b => b.UsuarioId == usuarioId && b.JogoId == jogoId);
    }

    public async Task Adicionar(Biblioteca biblioteca)
    {
        _dbContext.Bibliotecas.Add(biblioteca);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Remover(Biblioteca biblioteca)
    {
        _dbContext.Bibliotecas.Remove(biblioteca);
        await _dbContext.SaveChangesAsync();
    }
}