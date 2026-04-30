using FCG.Domain.Entity;
using FCG.Domain.Interfaces.Repositories;
using FCG.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Repositories;

public class JogoRepository : IJogoRepository
{
    private readonly FCGDbContext _dbContext;

    public JogoRepository(FCGDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Atualizar(Jogo entidade)
    {
        _dbContext.Jogos.Update(entidade);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Jogo> BuscarPorId(Guid id)
    {
        return await _dbContext.Jogos.FindAsync(id) ?? throw new KeyNotFoundException($"Jogo com ID {id} não encontrado.");
    }

    public async Task<IEnumerable<Jogo>> BuscarTodos()
    {
        return await _dbContext.Jogos.ToListAsync();
    }

    public async Task Criar(Jogo entidade)
    {
        _dbContext.Jogos.Add(entidade);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Deletar(Guid id)
    {
        var jogo = await _dbContext.Jogos.FindAsync(id);

        if (jogo == null)
        {
            throw new KeyNotFoundException($"Jogo com ID {id} não encontrado.");
        }

        _dbContext.Jogos.Remove(jogo);
        await _dbContext.SaveChangesAsync();
    }
}
