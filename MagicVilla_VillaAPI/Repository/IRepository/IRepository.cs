﻿using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IRepository<T>  where T : class   
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string includeProperty = null);

        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = false, string includeProperty = null);

        Task CreateAsync(T entity);

        Task Remove(T entity);

        Task SaveAsync();
    }
}
