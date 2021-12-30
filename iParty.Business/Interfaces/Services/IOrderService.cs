using iParty.Business.Infra;
using iParty.Business.Models.Orders;
using System;

namespace iParty.Business.Interfaces.Services
{
    public interface IOrderService : IService<Order>
    {
        public ServiceResult<Order> Create(Order order);

        public ServiceResult<Order> Update(Guid id, Order order);

        public ServiceResult<Order> AddOrderItem(Guid orderId, OrderItem orderItem);

        public ServiceResult<Order> ReplaceOrderItem(Guid orderId, Guid orderItemId, OrderItem orderItem);

        public ServiceResult<Order> RemoveOrderItem(Guid orderId, Guid orderItemId);
    }
}
