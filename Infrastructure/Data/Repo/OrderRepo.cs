using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.Entities;
using Domains.IRepo;
using Domains.Repo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repo
{
    public class OrderRepo : IOrderRepo
    {
        private readonly IGenericsRepo<BasketItem> _BasketItem;
        private readonly StoreContext _context;
        // private readonly IProductRepo _productRepo;
        public OrderRepo(IGenericsRepo<BasketItem> BasketItem,
            StoreContext context
        // IProductRepo productRepo
        )
        {
            _context = context;
            // _productRepo = productRepo;
            _BasketItem = BasketItem;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string UserId, Address shippingAddress)
        {
            // get basket from the repo
            var basket = await _BasketItem.ListAllAsync();

            // create order items
            var items = new List<OrderItem>();
            foreach (var item in basket.Where(x => x.UserId == UserId))
            {
                var orderItem = new OrderItem(item.Id, item.Product.Name, item.Product.PictureUrl, item.Product.Price, item.Quantity);
                items.Add(orderItem);
            }

            // create order
            var order = new Order(items, buyerEmail, shippingAddress);

            _context.Add(order);

            // save to db
            if (await _context.SaveChangesAsync() > 0)
                return order;
            return null;
        }


        public async Task<Order> GetOrderByIdAsync(Guid id, string buyerEmail)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            return await _context.Orders.Where(x => x.BuyerEmail == buyerEmail).ToListAsync();
        }
    }
}