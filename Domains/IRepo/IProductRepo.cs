using System;
using System.Threading.Tasks;
using Domains.Entities;
using Domains.Params;
using Domains.Utils;

namespace Domains.Repo
{
    public interface IProductRepo
    {
        Task<Product> GetProductById(Guid Id);
        Task<PageList<Product>> GetProducts(GetProductParams p);
    }
}