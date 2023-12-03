using Data.Entities;

namespace Data.Repositories.Interfaces;

public interface IRepository<T> where T : Entity
{
    public Task<List<T>> GetAll();
    public Task<T> GetById(int id);
    public Task DeleteById(int id);
    public Task Update(T entity);
    public Task<T> Add(T entity);
}