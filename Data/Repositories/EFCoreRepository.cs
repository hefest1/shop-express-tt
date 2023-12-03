using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public abstract class EFCoreRepository<T> : IRepository<T> where T : Entity
{
    protected abstract DbSet<T> TargetEntity { get; set; }
    protected readonly ToDoDBContext Context;

    public EFCoreRepository(ToDoDBContext context)
    {
        Context = context;
    }

    public async Task<List<T>> GetAll()
    {
	    var entities = await TargetEntity.ToListAsync();
	    return entities;
    }

    public async Task<T> GetById(int id)
    {
        var entity = await TargetEntity.SingleOrDefaultAsync(x => x.Id == id);
        return entity;
    }

    public async Task DeleteById(int id)
    {
        var entity = Activator.CreateInstance<T>();
        entity.Id = id;
        TargetEntity.Attach(entity);
        TargetEntity.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        TargetEntity.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<T> Add(T entity)
    {
        await TargetEntity.AddAsync(entity);
        await Context.SaveChangesAsync();

        return entity;
    }
}