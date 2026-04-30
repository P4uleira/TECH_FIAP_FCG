using FCG.Domain.Entity;
using FCG.Domain.Interfaces.Repositories;
using FCG.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Repositories;

public class UsuarioRepository : IUsuarioRepository
{

    private readonly FCGDbContext _dbContext;

    public UsuarioRepository(FCGDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Atualizar(Usuario entidade)
    {
        _dbContext.Usuarios.Update(entidade);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Usuario> BuscarPorId(Guid id)
    {
        return await _dbContext.Usuarios.FindAsync(id) ?? throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");
    }

    public async Task<IEnumerable<Usuario>> BuscarTodos()
    {
        return await _dbContext.Usuarios.ToListAsync();
    }

    public async Task Criar(Usuario entidade)
    {
        _dbContext.Usuarios.Add(entidade);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Deletar(Guid id)
    {
        var usuario = await _dbContext.Usuarios.FindAsync(id);

        if (usuario == null)
        {
            throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");
        }

        _dbContext.Usuarios.Remove(usuario);
        await _dbContext.SaveChangesAsync();
    }
    
}
