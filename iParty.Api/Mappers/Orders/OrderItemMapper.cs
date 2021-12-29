using AutoMapper;
using iParty.Api.Dtos.Orders;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mapppers;
using iParty.Api.Views.Items;
using iParty.Api.Views.Orders;
using iParty.Business.Infra.Extensions;
using iParty.Business.Interfaces;
using iParty.Business.Models.Items;
using iParty.Business.Models.Orders;
using System;
using System.Collections.Generic;

namespace iParty.Api.Mappers.Orders
{
    public class OrderItemMapper : BaseMapper<OrderItem>, IOrderItemMapper
    {
        private IRepository<Item> _itemRepository;

        private IMapper _autoMapper;

        public OrderItemMapper(IRepository<Item> itemRepository, IMapper autoMapper)
        {
            _itemRepository = itemRepository;
            _autoMapper = autoMapper;
        }

        public MapperResult<OrderItem> Map(OrderItemDto dto)
        {
            var item = _itemRepository.RecoverById(dto.ItemId).IfNull(() => { AddError("O item informado não existe."); });

            if (!SuccessResult()) return GetResult();

            SetEntity(new OrderItem()
            {
                Item = item,
                Price = dto.Price,
                Quantity = dto.Quantity
            });

            return GetResult();
        }

        public OrderItemView Map(OrderItem orderItem)
        {
            return mapToView(orderItem);
        }        

        public List<OrderItemView> Map(List<OrderItem> orderItems)
        {
            var items = new List<OrderItemView>();

            foreach (var orderItem in orderItems)
            {
                items.Add(mapToView(orderItem));
            }

            return items;
        }

        private OrderItemView mapToView(OrderItem orderItem)
        {
            if (orderItem == null) return null;

            var orderItemView = new OrderItemView()
            { 
                Id = orderItem.Id,
                Item = _autoMapper.Map<ItemSummarizedView>(orderItem.Item),
                Price = orderItem.Price,
                Quantity = orderItem.Quantity,
                Total = orderItem.Total
            };

            return orderItemView;
        }
    }
}
