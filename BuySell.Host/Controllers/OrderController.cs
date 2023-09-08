using AutoMapper;
using BuySell.Business.Services.Contracts;
using BuySell.Contracts.DTOs.Order;
using BuySell.Host.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BuySell.Host.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("addItem")]
        [Authorize(Policy = "Buyer")]
        public async Task<IActionResult> AddItem([FromBody]AddOrderItemDto dto)
        {
            var order = await _orderService.AddItem(User.GetUserId(), dto);
            return Ok(_mapper.Map<OrderViewDto>(order));
        }

        [HttpPost("removeItem")]
        [Authorize(Policy = "Buyer")]
        public async Task<IActionResult> RemoveItem([FromBody] RemoveOrderItemFromOrderDto dto)
        {
            var order = await _orderService.RemoveItem(User.GetUserId(), dto);
            return Ok(_mapper.Map<OrderViewDto>(order));
        }

        [HttpGet]
        [Authorize(Policy = "Buyer")]
        public async Task<IActionResult> GetCurrentOrder()
        {
            return Ok(_mapper.Map<OrderViewDto>(await _orderService.GetOrder(User.GetUserId())));
        }
    }
}
