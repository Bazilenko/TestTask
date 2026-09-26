
using TestTask.DAL.Entities;

namespace TestTask.DAL.Interfaces;
public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetById(int id, CancellationToken ct);
    Task<List<TEntity>> GetAllAsync(CancellationToken ct);
    Task AddAsync(TEntity entity, CancellationToken ct);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void HardDelete(TEntity entity);
}
