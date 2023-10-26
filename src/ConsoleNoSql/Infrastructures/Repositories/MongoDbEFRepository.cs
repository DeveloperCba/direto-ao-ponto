using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ConsoleNoSql.Infrastructures.Repositories;

public class MongoDbEFRepository<TEntity> : IMongoDbEFRepository<TEntity> where TEntity : class, new()
{
    protected readonly DbContext Context;
    internal DbSet<TEntity> dbSet;

    protected MongoDbEFRepository(
        DbContext context
    )
    {
        Context = context;
        dbSet = Context.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(int id) => await dbSet.FindAsync(id);
    public async Task<TEntity> GetByIdAsync(string id) => await dbSet.FindAsync(id);
    public async Task<TEntity> GetByIdAsync(Guid id) => await dbSet.FindAsync(id);
    public async Task<TEntity> GetByIdsAsync(object[] ids) => await dbSet.FindAsync(ids);

    public async Task<TEntity> GetByFilterAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Expression<Func<TEntity, TEntity>> field = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool isTracking = false)
    {
        IQueryable<TEntity> query = dbSet;
        if (filter != null)
            query = query.Where(filter);

        if (include != null)
            query = include(query);

        if (field != null)
            query = query.Select(field);

        if (!isTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Expression<Func<TEntity, TEntity>> field = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool isTracking = false)
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
            query = query.Where(filter);

        if (include != null)
            query = include(query);

        if (field != null)
            query = query.Select(field);

        if (orderBy != null)
            return orderBy(query);

        if (!isTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllPaginationAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Expression<Func<TEntity, TEntity>> field = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool isTracking = false,
        int pageNumber = 1,
        int pageSize = 10
    )
    {
        IQueryable<TEntity> query = dbSet;
        if (filter != null)
            query = query.Where(filter);

        if (include != null)
            query = include(query);

        if (field != null)
            query = query.Select(field);

        if (orderBy != null)
            return orderBy(query);

        if (!isTracking)
            query = query.AsNoTracking();

        query = query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<bool> HasValueAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        if (filter == null)
            return (await dbSet.CountAsync()) > 0;

        return (await dbSet.Where(filter).CountAsync()) > 0;

    }

    public async Task AddAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
    }

    public async Task AddRangerAsync(IEnumerable<TEntity> entities)
    {
        await dbSet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await Task.Run(() => dbSet.Update(entity));
    }

    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        await Task.Run(() => dbSet.UpdateRange(entities));
    }

    public async Task RemoveAsync(TEntity entity)
    {
        await Task.Run(() => dbSet.Remove(entity));
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        await Task.Run(() => dbSet.RemoveRange(entities));
    }

    public async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync();

    public void Dispose()
    {
        Context?.Dispose();
    }

}