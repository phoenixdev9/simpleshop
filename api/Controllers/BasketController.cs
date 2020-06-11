using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTO;
using api.Error;
using api.Extensions;
using AutoMapper;
using Domains.Entities;
using Domains.IRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domains.Repo;
using System.Linq;
using System;

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

        [HttpGet]
        public async Task<ActionResult<List<ReturnBasket>>> GetBaskets()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized(new ErrorRes(401));
            return Ok(_mapper.Map<List<BasketItem>, List<ReturnBasket>>(user.BasketItems.ToList()));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasketItem(Guid Id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized(new ErrorRes(401));
            var item = await _BasketItem.GetByIdAsync(Id);
            if (item == null)
                return NotFound(new ErrorRes(404));
            if (item.UserId != user.Id)
                return Unauthorized(new ErrorRes(401));
            _BasketItem.Delete(item);
            if (await _BasketItem.SaveAll())
                return Ok();
            return BadRequest(new ErrorRes(400, "Not able to delete the product from the cart"));
        }
        [HttpPost]
        public async Task<ActionResult> AddBasket(BasketDTO basket)
        {
            //Check product exists
            var p = await _product.GetProductById(basket.ProductId);
            if (p == null)
                return NotFound(new ErrorRes(404));
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
                return Ok();
            return BadRequest(new ErrorRes(400));
        }



    }
}