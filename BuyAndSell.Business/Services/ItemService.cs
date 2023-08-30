using AutoMapper;
using BuySell.Business.Services.Contracts;
using BuySell.Contracts.DTOs.Item;
using BuySell.Contracts.Exceptions;
using BuySell.Data.Entities;
using BuySell.Data.Repositories;
using BuySell.Data.Repositories.Contracts;
using BuySell.Data.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Services
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;

        public ItemService(IMapper mapper, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }
        public async Task<Item> AddItem(ItemCreateDto newItem, long userId)
        {
            var newItemEntity = _mapper.Map<Item>(newItem);
            newItemEntity.CreatedByUserId = userId;
            newItemEntity.UpdatedByUserId = userId;

            return await _itemRepository.CreateAsync(newItemEntity);
        }

        public async Task DeleteItem(long itemId, long userId)
        {
            var item = await _itemRepository.GetAsync(itemId) ??
                throw new NotFoundException("Nije pronadjen item sa zadatim id-jem");

            if(item.CreatedByUserId != userId)
            {
                throw new MethodNotAllowedException("Nije moguce obrisati tudji item");
            }

            await _itemRepository.DeleteAsync(item);
        }

        public async Task<ItemListViewDto> GetItems(long? sellerId, Query query)
        {
            query.AsNoTracking = true;
            var items = sellerId.HasValue ? await _itemRepository.GetAllAsync(query, sellerId.Value) :
                                            await _itemRepository.GetAllAsync(query);
            long count = await _itemRepository.CountAllAsync(query);

            var response = new ItemListViewDto()
            {
                Count = count,
                Items = _mapper.Map<IEnumerable<ItemViewDto>>(items)
            };

            return response;
        }

        public async Task<Item> UpdateItem(long itemId, ItemUpdateDto item, long userId)
        {
            var existing = await _itemRepository.GetAsync(itemId) ??
                throw new NotFoundException("Nije pronadjen item sa zadatim id-jem");

            _mapper.Map(item, existing);
            existing.UpdatedAtUtc = DateTime.UtcNow;
            existing.UpdatedByUserId = userId;

            await _itemRepository.UpdateAsync(existing);

            return existing;
        }
    }
}
