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
            var result = mapToEntity(dto);

            foreach (var erro in result.Errors) AddError(erro);

            SetEntity(result.Entity);

            return GetResult();
        }

        public List<MapperResult<OrderItem>> Map(List<OrderItemDto> dtos)
        {
            var result = new List<MapperResult<OrderItem>>();

            foreach (var dto in dtos)
            {                
                result.Add(mapToEntity(dto));
            }

            return result;
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

        private MapperResult<OrderItem> mapToEntity(OrderItemDto dto)
        {
            var result = new MapperResult<OrderItem>();

            var item = _itemRepository.RecoverById(dto.ItemId).IfNull(() => { result.Errors.Add("O item informado não existe."); });

            if (!result.Success) return result;

            result.DefineEntity(new OrderItem(_autoMapper.Map<ItemForOrder>(item), item.Unit, dto.Quantity, item.Price));

            return result;
        }

        private OrderItemView mapToView(OrderItem orderItem)
        {
            if (orderItem == null) return null;

            var orderItemView = new OrderItemView()
            { 
                Id = orderItem.Id,
                Version = orderItem.Version,
                Item = _autoMapper.Map<ItemSummarizedView>(orderItem.Item),
                Price = orderItem.Price,
                Unit = orderItem.Unit,
                Quantity = orderItem.Quantity,
                Total = orderItem.Total
            };

            return orderItemView;
        }
    }
}
