using iParty.Api.Dtos.Orders;
using iParty.Api.Infra;
using iParty.Api.Views.Orders;
using iParty.Business.Models.Orders;
using System.Collections.Generic;

namespace iParty.Api.Interfaces.Mapppers
{
    public interface IOrderItemMapper
    {
        MapperResult<OrderItem> Map(OrderItemDto dto);

        List<MapperResult<OrderItem>> Map(List<OrderItemDto> dto);

        OrderItemView Map(OrderItem orderItem);

        List<OrderItemView> Map(List<OrderItem> orderItems);
    }
}
