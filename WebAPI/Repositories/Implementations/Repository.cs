using System.Linq.Expressions;
using WebAPI.Database;
using WebAPI.Models.Common;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDb _appDb;

    public Repository(AppDb appDb)
    {
        _appDb = appDb;
    }

    public async Task CreateAsync(T entity)
    {
       await _appDb.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _appDb.Set<T>().Remove(entity);
    }

    public IQueryable<T> GetAll()
    {
        return _appDb.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _appDb.Set<T>().FindAsync(id);
    }

    public IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression)
    {
        return _appDb.Set<T>().Where(expression);
    }

    public async Task<int> SaveAsync()
    {
        return await _appDb.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _appDb.Set<T>().Update(entity);
    }
}
