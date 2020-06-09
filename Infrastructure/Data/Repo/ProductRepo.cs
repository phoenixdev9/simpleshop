using System;
using System.Linq;
using System.Threading.Tasks;
using Domains.Entities;
using Domains.Params;
using Domains.Repo;
using Domains.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repo {

    public class ProductRepo : IProductRepo {
        private readonly StoreContext _context;
        public ProductRepo (StoreContext context) {
            _context = context;
        }
        public IQueryable<Product> GetProductsList (GetProductParams param) {
            const string asce = "priceAsc";
            const string desc = "priceDesc";

            var p = _context.Products.Include (x => x.ProductBrand).Include (x => x.ProductType).AsQueryable ();
            p = p.OrderBy (x => x.Name);
            if (param.BrandId != null) {
                p = p.Where (x => x.ProductBrandId == param.BrandId);
            }
            if (param.TypeId != null) {
                p = p.Where (x => x.ProductTypeId == param.TypeId);
            }
            if (string.IsNullOrEmpty (param.Search) == false) {
                p = p.Where (x => x.Name.ToLower ().Contains (param.Search.ToLower ()));
            }
            if (string.IsNullOrEmpty (param.Sort) == false) {
                switch (param.Sort) {
                    case asce:
                        p = p.OrderBy (x => x.Price);
                        break;
                    case desc:
                        p = p.OrderByDescending (x => x.Price);
                        break;
                    default:
                        p = p.OrderBy (x => x.Name);
                        break;
                }
            }

            return p;
        }
        public async Task<Product> GetProductById (Guid Id) {
            var p = new GetProductParams ();
            return await GetProductsList (p).FirstOrDefaultAsync (x => x.Id == Id);
        }

        public async Task<PageList<Product>> GetProducts (GetProductParams p) {
            var list = GetProductsList (p);
            return await PageList<Product>.CreateAsync (list, p.PageIndex, p.PageSize);
        }

    }
}