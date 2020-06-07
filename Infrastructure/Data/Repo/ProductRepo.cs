using System;
using System.Collections.Generic;
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

        public async Task<Product> GetProductById(Guid Id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);
        }
        // public async Task<Product> CreateProduct(InputCreateProductDTO data)
        // {
        //     return await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);
        // }

        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

    }
}