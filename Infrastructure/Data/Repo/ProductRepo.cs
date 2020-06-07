using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.Entities;
using Domains.Repo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repo
{

    public class ProductRepo : IProductRepo
    {
        private readonly StoreContext _context;
        public ProductRepo(StoreContext context)
        {
            _context = context;
        }
        public IQueryable<Product> GetProductsList()
        {
            return _context.Products.Include(x => x.ProductBrand).Include(x => x.ProductType).AsQueryable();
        }
        public async Task<Product> GetProductById(Guid Id)
        {
            return await GetProductsList().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            return await GetProductsList().ToListAsync();
        }

    }
}