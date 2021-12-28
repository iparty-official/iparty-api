using iParty.Business.Infra;
using iParty.Business.Models.Orders;
using System;

namespace iParty.Business.Interfaces.Services
{
    public interface IOrderService : IService<Order>
    {
        public ServiceResult<Order> Create(Order order);

        public ServiceResult<Order> Update(Guid id, Order order);
    }
}
