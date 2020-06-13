using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.Entities;
using Domains.IRepo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repo {
    public class GenericsRepo<T> : IGenericsRepo<T> where T : Base {
        private readonly StoreContext _context;
        public GenericsRepo (StoreContext context) {
            _context = context;
        }
        public async Task<T> GetByIdAsync (Guid id) {
            return await _context.Set<T> ().FindAsync (id);
        }
        public void Add (T entity) {
            _context.Set<T> ().Add (entity);
        }

        public void Update (T entity) {
            _context.Set<T> ().Attach (entity);
            _context.Entry (entity).State = EntityState.Modified;
        }

        public void Delete (T entity) {
            _context.Set<T> ().Remove (entity);
        }
        public async Task<IQueryable<T>> ListAllAsync () {
            var list = await _context.Set<T> ().ToListAsync ();

            return list.AsQueryable ();
        }
        public async Task<bool> SaveAll () {
            return await _context.SaveChangesAsync () > 0;
        }
    }
}