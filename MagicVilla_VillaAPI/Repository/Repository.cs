using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private const int MaxPageSize = 100;
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //_db.VillaNumbers.Include(u => u.Villa).ToList();
            this._dbSet = db.Set<T>();
        }
        
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }

        //"Villa, VillaSpecial"
        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = false, string includeProperty = null)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //To include the property if there is any. (In this example Villa)
            if (includeProperty == null) return await query.FirstOrDefaultAsync();
            query = includeProperty.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProp) => current.Include(includeProp));


            return await query.FirstOrDefaultAsync();
        }


        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string includeProperty = null, int pageSize = 0, int pageNumber = 1)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (pageSize > 0)
            {
                if (pageSize > MaxPageSize)
                {
                    pageSize = MaxPageSize;
                }
                query = query.Skip(pageSize * (pageNumber - 1 )).Take(pageSize);
            }

            if (includeProperty == null) return await query.ToListAsync();
            query = includeProperty.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProp) => current.Include(includeProp));


            return await query.ToListAsync();
        }

        public async Task Remove(T entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
