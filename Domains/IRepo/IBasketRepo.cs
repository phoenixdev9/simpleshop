using System.Collections.Generic;
using System.Threading.Tasks;
using Domains.Entities;

namespace Domains.IRepo
{
    public interface IBasketRepo
    {
        Task<ICollection<BasketItem>> GetCarts();
        Task<BasketItem> AddCart();
        Task<BasketItem> UpdateCart();
        Task DeleteCart();

    }
}