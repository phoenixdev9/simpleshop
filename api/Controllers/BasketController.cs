using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.Error;
using api.Extensions;
using AutoMapper;
using Domains.Entities;
using Domains.IRepo;
using Domains.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    public class BasketController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IGenericsRepo<BasketItem> _BasketItem;
        private readonly IProductRepo _product;

        public BasketController(UserManager<User> userManager, IMapper mapper, IGenericsRepo<BasketItem> BasketItem, IProductRepo Product)
        {
            _mapper = mapper;
            _userManager = userManager;
            _BasketItem = BasketItem;
            _product = Product;
        }
        /////////////////////////////////////////////////////
        private async Task<BasketItem> FindProductFromBasket(Guid productId, string userId)
        {
            var items = await _BasketItem.ListAllAsync();
            return items.FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);
        }
        private async Task<List<BasketItem>> GetProductFromBasket(string userId)
        {
            var items = await _BasketItem.ListAllAsync();
            return items.Where(x => x.UserId == userId).ToList();
        }
        private decimal getTotal(IReadOnlyList<ReturnBasket> basketItems)
        {
            decimal total = 0;
            for (int i = 0; i < basketItems.Count; i++)
            {
                var currentProduct = basketItems.ElementAt(i);
                total += currentProduct.Price * currentProduct.Quantity;
            }
            return total;
        }
        /////////////////////////////////////////////////////
        [HttpGet]
        public async Task<ActionResult<ReturnCheckout>> GetBaskets()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized(new ErrorRes(401));
            var basketOfReturnProducts = _mapper.Map<List<BasketItem>, List<ReturnBasket>>(await GetProductFromBasket(user.Id));
            return Ok(new ReturnCheckout(basketOfReturnProducts, getTotal(basketOfReturnProducts)));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReturnCheckout>> DeleteBasketItem(Guid Id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized(new ErrorRes(401));

            var item = await FindProductFromBasket(Id, user.Id);
            if (item == null)
                return NotFound(new ErrorRes(404));

            _BasketItem.Delete(item);
            if (await _BasketItem.SaveAll())
            {
                var basketOfReturnProducts = _mapper.Map<IReadOnlyList<BasketItem>, IReadOnlyList<ReturnBasket>>(await GetProductFromBasket(user.Id));

                return Ok(new ReturnCheckout(basketOfReturnProducts, getTotal(basketOfReturnProducts)));
            }
            return BadRequest(new ErrorRes(400, "Not able to delete the product from the cart"));
        }

        [HttpPost]
        public async Task<ActionResult<ReturnCheckout>> AddBasket(BasketDTO basket)
        {
            //Check product exists
            var p = await _product.GetProductById(basket.ProductId);
            if (p == null)
                return NotFound(new ErrorRes(404, "Product doesn't exist"));
            //Find the User
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized(new ErrorRes(401));
            //Check if user has the product or not
            var item = user.BasketItems.FirstOrDefault(x => x.ProductId == basket.ProductId);
            if (item == null)
            {
                _BasketItem.Add(new BasketItem
                {
                    Product = p,
                    Quantity = basket.Quantity,
                    User = user,
                });
            }
            else
            {
                item.Quantity = basket.Quantity;
                _BasketItem.Update(item);
            }
            if (await _BasketItem.SaveAll())
            {
                var basketOfReturnProducts = _mapper.Map<IReadOnlyList<BasketItem>, IReadOnlyList<ReturnBasket>>(await GetProductFromBasket(user.Id));
                return Ok(new ReturnCheckout(basketOfReturnProducts, getTotal(basketOfReturnProducts)));
            }
            return BadRequest(new ErrorRes(400));
        }

    }
}