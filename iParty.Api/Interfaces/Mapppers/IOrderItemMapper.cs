using iParty.Api.Dtos.Orders;
using iParty.Api.Infra;
using iParty.Business.Models.Orders;

namespace iParty.Api.Interfaces.Mapppers
{
    public interface IOrderItemMapper
    {
        MapperResult<OrderItem> Map(OrderItemDto dto);
    }
}
