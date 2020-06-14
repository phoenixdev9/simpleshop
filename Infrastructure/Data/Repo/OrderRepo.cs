using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.Entities;
using Domains.IRepo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repo {
    public class OrderRepo : IOrderRepo {
        private readonly StoreContext _context;
        public OrderRepo (
            StoreContext context
        ) {
            _context = context;
        }
        public async Task<Order> GetOrderByIdAsync (Guid id) {
            return await _context.Orders.Include (x => x.ShipToAddress).Include (x => x.OrderItems).FirstOrDefaultAsync (x => x.Id == id);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync (string buyerEmail) {
            return await _context.Orders.Include (x => x.ShipToAddress).Include (x => x.OrderItems).Where (x => x.BuyerEmail == buyerEmail).OrderByDescending (x => x.OrderDate).ToListAsync ();
        }
    }
}