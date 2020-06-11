using System.Collections.Generic;
using System.Threading.Tasks;
using Domains.Entities;
using Domains.IRepo;

namespace Infrastructure.Data.Repo
{
    public class BasketRepo : IBasketRepo
    {
        public Task<BasketItem> AddCart()
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteCart()
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<BasketItem>> GetCarts()
        {
            throw new System.NotImplementedException();
        }

        public Task<BasketItem> UpdateCart()
        {
            throw new System.NotImplementedException();
        }
    }
}