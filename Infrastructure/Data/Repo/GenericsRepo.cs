using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domains.Entities;
using Domains.IRepo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repo
{
    public class GenericsRepo<T> : IGenericsRepo<T> where T : Base
    {
        private readonly StoreContext _context;
        public GenericsRepo(StoreContext context)
        {
            _context = context;
        }
        // public async Task<T> GetByIdAsync (Guid id) {
        //     return await _context.Set<T> ().FindAsync (id);
        // }
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}