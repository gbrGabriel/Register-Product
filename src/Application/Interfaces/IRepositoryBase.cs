namespace Application.Interfaces;
public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
{
    Task<int> SaveChangesAsync();
    Task<int> UpdateAsync(TEntity entity);
    Task<int> DeleteAsync(TEntity entity);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entity);
    Task<TEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
}
