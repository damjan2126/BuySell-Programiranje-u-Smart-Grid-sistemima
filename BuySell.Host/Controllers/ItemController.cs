using AutoMapper;
using BuySell.Business.Services.Contracts;
using BuySell.Contracts.DTOs.Item;
using BuySell.Data.Resources;
using BuySell.Host.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuySell.Host.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;

        public ItemController(IMapper mapper, IItemService itemService)
        {
            _mapper = mapper;
            _itemService = itemService;
        }

        [HttpPost]
        [Authorize(Policy = "ActiveSeller")]
        public async Task<IActionResult> CreateItem([FromBody] ItemCreateDto item)
        {
            var createdItem = await _itemService.AddItem(item, User.GetUserId());

            return Ok(_mapper.Map<ItemViewDto>(createdItem));
        }

        [HttpPost("{id}")]
        [Authorize(Policy = "ActiveSeller")]
        public async Task<IActionResult> UpdateItem(long id, [FromBody] ItemUpdateDto item)
        {
            var updatedItem = await _itemService.UpdateItem(id, item, User.GetUserId());

            return Ok(_mapper.Map<ItemViewDto>(updatedItem));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "ActiveSeller")]
        public async Task<IActionResult> Delete(long id)
        {
            await _itemService.DeleteItem(id, User.GetUserId());

            return Ok();
        }

        [HttpGet("{id:long?}")]
        [Authorize(Policy = "Active")]
        public async Task<IActionResult> GetItems(long? id, [FromBody]Query query)
        {
            return Ok(_mapper.Map<ItemViewDto>(await _itemService.GetItems(id, query)));
        }
    }
}
