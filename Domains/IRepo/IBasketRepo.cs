using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domains.Entities;

namespace Domains.IRepo {
    public interface IBasketRepo {
        Task<IReadOnlyList<BasketItem>> GetCarts (string userid);
        decimal GetTotal (IReadOnlyList<BasketItem> basketItems);
        int getTotalItems (IReadOnlyList<BasketItem> basketItems);
        Task<BasketItem> GetProductFromBasket (Guid productId, string UserId);

    }
}