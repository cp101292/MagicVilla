using MagicVilla_VillaAPI.Model;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IRepository<T>  where T : class   
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);

        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = false);

        Task CreateAsync(T entity);

        Task Remove(T entity);

        Task SaveAsync();
    }
}
