namespace FCG.Domain.Interfaces.Repositories;

public interface IRepository <T> where T : class
{
    Task<T> BuscarPorId(Guid id);
    Task<IEnumerable<T>> BuscarTodos();
    Task Criar(T entidade);
    Task Atualizar(T entidade);
    Task Deletar(Guid id);
}