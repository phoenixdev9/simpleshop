using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.Entities;
using Domains.IRepo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repo {
    public class BasketRepo : IBasketRepo {
        private readonly StoreContext _context;
        public BasketRepo (
            StoreContext context
        ) {
            _context = context;
        }

        public async Task<IReadOnlyList<BasketItem>> GetCarts (string userid) {
            return await _context.BasketItems.Where (x => x.UserId == userid).Include (x => x.Product).ToListAsync ();
        }

        public async Task<BasketItem> GetProductFromBasket (Guid productId, string UserId) {
            return await _context.BasketItems.Include (x => x.Product).FirstOrDefaultAsync (x => x.ProductId == productId && x.UserId == UserId);
        }

        public decimal GetTotal (IReadOnlyList<BasketItem> basketItems) {
            decimal total = 0;
            for (int i = 0; i < basketItems.Count; i++) {
                var currentProduct = basketItems.ElementAt (i);
                total += currentProduct.Product.Price * currentProduct.Quantity;
            }
            return total;
        }

        public int getTotalItems (IReadOnlyList<BasketItem> basketItems) {
            int total = 0;
            for (int i = 0; i < basketItems.Count; i++) {
                var currentProduct = basketItems.ElementAt (i);
                total += currentProduct.Quantity;
            }
            return total;
        }
    }
}