using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTO;
using api.Error;
using api.Extensions;
using API.Extensions;
using AutoMapper;
using Domains.Entities;
using Domains.IRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IOrderRepo _orderRepo;
        private readonly IGenericsRepo<Address> _address;
        private readonly IBasketRepo _basketRepo;
        private readonly IGenericsRepo<Order> _orderGenericsRepo;

        public OrderController(IGenericsRepo<Order> OrderGenericsRepo, IBasketRepo basketRepo, UserManager<User> userManager, IMapper mapper, IOrderRepo orderRepo, IGenericsRepo<Address> addressRepo)
        {
            _mapper = mapper;
            _userManager = userManager;
            _orderRepo = orderRepo;
            _address = addressRepo;
            _basketRepo = basketRepo;
            _orderGenericsRepo = OrderGenericsRepo;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder()
        {

            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized(new ErrorRes(401));
            // get basket from the repo
            var basket = await _basketRepo.GetCarts(user.Id);
            var total = _basketRepo.GetTotal(basket);
            if (user.address == null) return Unauthorized(new ErrorRes(400, "You must fill your address to create an order"));
            // create order items
            var items = new List<OrderItem>();
            foreach (var item in basket)
            {
                var orderItem = new OrderItem(item.Id, item.Product.Name, item.Product.PictureUrl, item.Product.Price, item.Quantity);
                items.Add(orderItem);
            }

            // create order
            var order = new Order(items, user.Email, user.address, total);
            //Add Order
            _orderGenericsRepo.Add(order);
            // save to db
            if (await _orderGenericsRepo.SaveAll())
            {
                return Ok(_mapper.Map<Order, OrderReturnDTO>(order));
            }
            return BadRequest(new ErrorRes(400, "Problem creating order"));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderReturnDTO>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            if (email == null) return Unauthorized(new ErrorRes(401));
            var orders = await _orderRepo.GetOrdersForUserAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderReturnDTO>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReturnDTO>> GetOrderByIdForUser(Guid id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            if (email == null) return Unauthorized(new ErrorRes(401));
            var order = await _orderRepo.GetOrderByIdAsync(id);

            if (order == null) return NotFound(new ErrorRes(404, "Order is not found"));

            return _mapper.Map<Order, OrderReturnDTO>(order);
        }
    }
}