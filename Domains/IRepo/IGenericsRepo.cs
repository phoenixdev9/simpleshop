using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.Entities;

namespace Domains.IRepo
{
    public interface IGenericsRepo<T> where T : Base
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IQueryable<T>> ListAllAsync();
        // Task<T> GetEntityWithSpec (ISpecification<T> spec);
        // Task<IReadOnlyList<T>> ListAsync (ISpecification<T> spec);
        // Task<int> CountAsync (ISpecification<T> spec);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveAll();
    }
}