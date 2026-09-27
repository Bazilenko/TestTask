
using TestTask.DAL.Data;
using TestTask.DAL.Entities;
using TestTask.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TestTask.DAL.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity, CancellationToken ct)
    {
        await _dbSet.AddAsync(entity, ct);
    }

    public void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
        Update(entity);
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
    {
        return await _dbSet.ToListAsync(ct);
    }

    public async Task<TEntity?> GetById(int id, CancellationToken ct)
    {
        return await _dbSet.FindAsync(id, ct);
    }

    public void HardDelete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    
}