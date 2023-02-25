namespace Templ.Domain;
public interface IRepository<TEntity> where TEntity : IAggregateRoot
{
    Task<TEntity?> FindById(Guid id);
    Task<TEntity?> FindByName(string name);
    Task<IEnumerable<TEntity>> FindAll();
    Task<TEntity> Add(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task<int> Remove(TEntity entity);
    Task SaveAsync();
}
