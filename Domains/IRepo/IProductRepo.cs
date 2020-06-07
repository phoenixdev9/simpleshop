using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domains.Entities;

namespace Domains.Repo
{
    public interface IProductRepo
    {
        Task<Product> GetProductById(Guid Id);
        Task<IReadOnlyList<Product>> GetProducts();
    }
}