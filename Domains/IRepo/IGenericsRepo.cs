using System;
using System.Linq;
using System.Threading.Tasks;
using Domains.Entities;

namespace Domains.IRepo {
    public interface IGenericsRepo<T> where T : Base {
        Task<T> GetByIdAsync (Guid id);
        Task<IQueryable<T>> ListAllAsync ();
        void Add (T entity);
        void Update (T entity);
        void Delete (T entity);
        Task<bool> SaveAll ();
    }
}