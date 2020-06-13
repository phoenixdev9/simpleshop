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

        public OrderController(UserManager<User> userManager, IMapper mapper, IOrderRepo orderRepo)
        {
            _mapper = mapper;
            _userManager = userManager;
            _orderRepo = orderRepo;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDto)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized(new ErrorRes(401));
            var address = _mapper.Map<AddressDTO, Address>(orderDto.ShipToAddress);

            var order = await _orderRepo.CreateOrderAsync(user.Email, user.Id, address);

            if (order == null) return BadRequest(new ErrorRes(400, "Problem creating order"));
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderReturnDTO>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            System.Console.WriteLine(email);
            var orders = await _orderRepo.GetOrdersForUserAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderReturnDTO>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReturnDTO>> GetOrderByIdForUser(Guid id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var order = await _orderRepo.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ErrorRes(404, "Order is not found"));

            return _mapper.Map<Order, OrderReturnDTO>(order);
        }
    }
}