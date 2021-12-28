using iParty.Api.Dtos.Orders;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mapppers;
using iParty.Business.Models.Orders;
using System;

namespace iParty.Api.Mappers.Orders
{
    public class OrderItemMapper : BaseMapper<OrderItem>, IOrderItemMapper
    {
        public MapperResult<OrderItem> Map(OrderItemDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
