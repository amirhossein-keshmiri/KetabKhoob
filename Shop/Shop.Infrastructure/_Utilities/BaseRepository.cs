using Common.Domain.Repository;
using Common.Domain;
using System.Linq.Expressions;
using Shop.Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;

namespace Shop.Infrastructure._Utilities;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ShopContext Context;
    public BaseRepository(ShopContext context)
    {
        Context = context;
    }

    void IBaseRepository<TEntity>.Add(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    public async Task AddRange(ICollection<TEntity> entities)
    {
        await Context.Set<TEntity>().AddRangeAsync(entities);
    }

    public bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().Any(expression);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Context.Set<TEntity>().AnyAsync(expression);
    }

    public TEntity? Get(long id)
    {
        return Context.Set<TEntity>().FirstOrDefault(t => t.Id.Equals(id)); ;
    }

    public async Task<TEntity?> GetAsync(long id)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(id)); ;
    }

    public async Task<TEntity?> GetTracking(long id)
    {
        return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
    }

    public async Task<int> Save()
    {
        return await Context.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        Context.Update(entity);
    }
}

