using iParty.Business.Infra;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Items;
using iParty.Business.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Business.Services.Orders
{
    public class OrderService : Service<Order, IRepository<Order>>, IOrderService
    {
        private IOrderValidation _orderValidation;

        private IOrderItemValidation _orderItemValidation;        

        public OrderService(IRepository<Order> rep, IOrderValidation orderValidation, IOrderItemValidation orderItemValidation) : base(rep)
        {
            _orderValidation = orderValidation;
            _orderItemValidation = orderItemValidation;         
        }

        public ServiceResult<Order> Create(Order order)
        {
            order.SetDefaultValuesForNewOrder();

            order.TotalizeOrder();

            var result = _orderValidation.CustomValidate(order);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(order);

            return GetSuccessResult(order);
        }        

        public ServiceResult<Order> Update(Guid id, Order newOrder)
        {            
            var currentOrder = Get(id);

            if (currentOrder == null)
                return GetFailureResult("Não foi possível localizar o pedido informado.");            

            currentOrder.CopyHeaderData(newOrder);

            currentOrder.Items.RemoveAll(x => itemRemoved(x.Item.Id, currentOrder, newOrder));

            currentOrder.Items.AddRange(itemsAdded(currentOrder, newOrder));

            currentOrder.CopyItemsData(newOrder);

            currentOrder.TotalizeOrder();

            var result = _orderValidation.CustomValidate(currentOrder);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, currentOrder);

            return GetSuccessResult(currentOrder);
        }        

        public ServiceResult<Order> AddOrderItem(Guid orderId, OrderItem orderItem)
        {            
            var order = Get(orderId);

            if (order == null)
                return GetFailureResult("Não foi possível localizar o pedido informado.");           

            var result = _orderItemValidation.Validate(orderItem);

            if (!result.IsValid)
                return GetFailureResult(result);

            order.Items.Add(orderItem);

            order.TotalizeOrder();

            Rep.Update(orderId, order);

            return GetSuccessResult(order);
        }

        public ServiceResult<Order> ReplaceOrderItem(Guid orderId, Guid orderItemId, OrderItem orderItem)
        {            
            var order = Get(orderId);

            if (order == null)
                return GetFailureResult("Não foi possível localizar o pedido informado.");

            var result = _orderItemValidation.Validate(orderItem);

            if (!result.IsValid)
                return GetFailureResult(result);
            
            order.ReplaceItem(orderItemId, orderItem);

            order.TotalizeOrder();

            Rep.Update(orderId, order);

            return GetSuccessResult(order);
        }

        public ServiceResult<Order> RemoveOrderItem(Guid orderId, Guid orderItemId)
        {            
            var order = Get(orderId);

            if (order == null)
                return GetFailureResult("Não foi possível localizar o pedido informado.");

            order.RemoveItem(orderItemId);

            order.TotalizeOrder();            

            Rep.Update(orderId, order);

            return GetSuccessResult(order);
        }

        private List<OrderItem> itemsAdded(Order currentOrder, Order newOrder)
        {
            var result = new List<OrderItem>();

            foreach (var item in newOrder.Items)
            {
                if (!currentOrder.Items.Exists(x => x.Item.Id == item.Item.Id))
                {
                    result.Add(item);
                }
            }

            return result;
        }
        private bool itemRemoved(Guid itemId, Order currentOrder, Order newOrder)
        {           
            return currentOrder.Items.Exists(x => x.Item.Id == itemId) && !newOrder.Items.Exists(x => x.Item.Id == itemId);
        }
    }
}
