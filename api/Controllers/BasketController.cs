using System;
using System.Collections.Generic;
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

namespace api.Controllers {
    [Authorize]
    public class BasketController : BaseController {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IBasketRepo _basketRepo;
        private readonly IProductRepo _productRepo;
        private readonly IGenericsRepo<BasketItem> _basketGenericRepo;

        public BasketController (UserManager<User> userManager, IMapper mapper, IGenericsRepo<BasketItem> basketGenericRepo, IBasketRepo basketRepo, IProductRepo productRepo) {
            _mapper = mapper;
            _userManager = userManager;
            _basketRepo = basketRepo;
            _productRepo = productRepo;
            _basketGenericRepo = basketGenericRepo;
        }

        [HttpGet (Name = "Get")]
        public async Task<ActionResult<ReturnCheckout>> GetBaskets () {
            var user = await _userManager.FindByEmailFromClaimsPrinciple (HttpContext.User);
            if (user == null) return Unauthorized (new ErrorRes (401));
            var basketItems = await _basketRepo.GetCarts (user.Id);
            return Ok (new ReturnCheckout (_mapper.Map<IReadOnlyList<BasketItem>, IReadOnlyList<ReturnBasket>> (basketItems), _basketRepo.GetTotal (basketItems), _basketRepo.getTotalItems (basketItems)));
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult<ReturnCheckout>> DeleteBasketItem (Guid id) {
            var user = await _userManager.FindByEmailFromClaimsPrinciple (HttpContext.User);
            if (user == null) return Unauthorized (new ErrorRes (401));

            var item = await _basketRepo.GetProductFromBasket (id, user.Id);
            if (item == null)
                return NotFound (new ErrorRes (404));

            _basketGenericRepo.Delete (item);
            if (await _basketGenericRepo.SaveAll ()) {
                return await GetBaskets ();

            }
            return BadRequest (new ErrorRes (400, "Not able to delete the product from the cart"));
        }

        [HttpDelete]
        public async Task<ActionResult<ReturnCheckout>> ClearBasket () {
            var user = await _userManager.FindByEmailFromClaimsPrinciple (HttpContext.User);
            if (user == null) return Unauthorized (new ErrorRes (401));

            var item = await _basketRepo.GetCarts (user.Id);
            foreach (var basketItem in item) {
                _basketGenericRepo.Delete (basketItem);
            }

            if (await _basketGenericRepo.SaveAll ()) {
                return await GetBaskets ();
            }
            return BadRequest (new ErrorRes (400, "Not able to delete the product from the cart"));
        }

        [HttpPost]
        public async Task<ActionResult<ReturnCheckout>> AddBasket (BasketDTO basket) {
            //Check product exists
            var p = await _productRepo.GetProductById (basket.ProductId);
            if (p == null)
                return NotFound (new ErrorRes (404, "Product doesn't exist"));
            //Find the User
            var user = await _userManager.FindByEmailFromClaimsPrinciple (HttpContext.User);
            if (user == null) return Unauthorized (new ErrorRes (401));
            //Check if user has the product or not
            var item = await _basketRepo.GetProductFromBasket (basket.ProductId, user.Id);
            if (item == null) {
                _basketGenericRepo.Add (new BasketItem {
                    Product = p,
                        Quantity = basket.Quantity,
                        User = user,
                });
            }
            //Update the basket item if exists
            else {
                item.Quantity = basket.Quantity;
                _basketGenericRepo.Update (item);
            }
            if (await _basketGenericRepo.SaveAll ()) {
                return await GetBaskets ();
            }
            return BadRequest (new ErrorRes (400));
        }

    }
}