using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domains.Entities;

namespace Domains.IRepo
{
    public interface IOrderRepo
    {
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdAsync(Guid id);
    }
}