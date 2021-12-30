using iParty.Api.Dtos.Orders;
using iParty.Api.Infra;
using iParty.Api.Views.Orders;
using iParty.Business.Models.Orders;
using System.Collections.Generic;

namespace iParty.Api.Interfaces.Mapppers
{
    public interface IOrderMapper
    {
        MapperResult<Order> Map(OrderDto dto);

        OrderView Map(Order order);

        List<OrderView> Map(List<Order> orders);
    }
}
