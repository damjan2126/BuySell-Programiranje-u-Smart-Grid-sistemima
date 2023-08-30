using BuySell.Contracts.DTOs.Item;
using BuySell.Data.Entities;
using BuySell.Data.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Services.Contracts
{
    public interface IItemService
    {
        Task<Item> AddItem(ItemCreateDto newItem, long userId);
        Task<Item> UpdateItem(long itemId, ItemUpdateDto item, long userId);
        Task DeleteItem(long itemId, long userId);
        Task<ItemListViewDto> GetItems(long? sellerId, Query query);
    }
}
