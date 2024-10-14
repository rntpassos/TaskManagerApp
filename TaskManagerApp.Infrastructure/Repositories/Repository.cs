using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Domain.Interfaces;

namespace TaskManagerApp.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class, IBaseEntity
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Func<T, bool> predicate)
    {
        return await Task.FromResult(_dbSet.Where(predicate).ToList());
    }

    async Task IRepository<T>.AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        int rowsAffected = await _dbSet
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync();

        if (rowsAffected == 0)
        {
            throw new KeyNotFoundException($"Entidade com id {id} não encontrada.");
        }
    }

    async Task<T?> IRepository<T>.GetByIdAsync(long id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"Entidade com id {id} não encontrada.");
        return entity;
    }

    async Task IRepository<T>.UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

}

