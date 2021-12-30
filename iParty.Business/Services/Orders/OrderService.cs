using iParty.Business.Infra;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Items;
using iParty.Business.Models.Orders;
using System;
using System.Linq;

namespace iParty.Business.Services.Orders
{
    public class OrderService : Service<Order, IRepository<Order>>, IOrderService
    {
        private IOrderValidation _orderValidation;

        private IOrderItemValidation _orderItemValidation;

        private IRepository<Item> _itemRepository;

        public OrderService(IRepository<Order> rep, IOrderValidation orderValidation, IOrderItemValidation orderItemValidation, IRepository<Item> itemRepository) : base(rep)
        {
            _orderValidation = orderValidation;
            _orderItemValidation = orderItemValidation;
            _itemRepository = itemRepository;
        }

        public ServiceResult<Order> Create(Order order)
        {
            order = loadDefaulValues(order);

            order = calculateOrder(order);

            var result = _orderValidation.CustomValidate(order);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(order);

            return GetSuccessResult(order);
        }        

        public ServiceResult<Order> Update(Guid id, Order order)
        {
            var currentOrder = Get(id);

            if (currentOrder == null)
                return GetFailureResult("Não foi possível localizar o pedido informado.");

            order = copyFromCurrentOrder(order, currentOrder);

            order = calculateOrder(order);

            var result = _orderValidation.CustomValidate(order);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, order);

            return GetSuccessResult(order);
        }       

        public ServiceResult<Order> AddOrderItem(Guid orderId, OrderItem orderItem)
        {
            //TODO: Recalcular totais do pedido aqui

            var order = Get(orderId);

            if (order == null)
                return GetFailureResult("Não foi possível localizar o pedido informado.");

            var result = _orderItemValidation.Validate(orderItem);

            if (!result.IsValid)
                return GetFailureResult(result);

            order.Items.Add(orderItem);            

            Rep.Update(orderId, order);

            return GetSuccessResult(order);
        }

        public ServiceResult<Order> ReplaceOrderItem(Guid orderId, Guid orderItemId, OrderItem orderItem)
        {
            //TODO: Recalcular totais do pedido aqui

            var order = Get(orderId);

            if (order == null)
                return GetFailureResult("Não foi possível localizar o pedido informado.");

            var result = _orderItemValidation.Validate(orderItem);

            if (!result.IsValid)
                return GetFailureResult(result);

            var replaceResult = replaceItem(order, orderId, orderItem);

            if (!replaceResult.Success) return replaceResult;

            Rep.Update(orderId, order);

            return GetSuccessResult(order);
        }

        public ServiceResult<Order> RemoveOrderItem(Guid orderId, Guid orderItemId)
        {
            //TODO: Recalcular totais do pedido aqui

            var order = Get(orderId);

            if (order == null)
                return GetFailureResult("Não foi possível localizar o pedido informado.");

            var removeResult = removeItem(order, orderItemId);

            if (!removeResult.Success) return removeResult;

            Rep.Update(orderId, order);

            return GetSuccessResult(order);
        }

        private ServiceResult<Order> replaceItem(Order order, Guid orderItemId, OrderItem newOrderItem)
        {
            var currentOrderItem = order.Items.Find(x => x.Id == orderItemId);

            if (currentOrderItem == null)
                return GetFailureResult("Não foi possível localizar o item de pedido informado.");

            var index = order.Items.IndexOf(currentOrderItem);

            order.Items.Remove(currentOrderItem);

            newOrderItem.Id = orderItemId;

            order.Items.Insert(index, newOrderItem);            

            return GetSuccessResult(order);
        }

        private ServiceResult<Order> removeItem(Order order, Guid orderItemId)
        {
            var currentOrderItem = order.Items.Find(x => x.Id == orderItemId);

            if (currentOrderItem == null)
                return GetFailureResult("Não foi possível localizar o item de pedido informado");

            var index = order.Items.IndexOf(currentOrderItem);

            order.Items.Remove(currentOrderItem);

            return GetSuccessResult(order);
        }

        private Order calculateOrder(Order order)
        {
            foreach (var item in order.Items)
            {
                item.Total = item.Quantity * item.Price;
            }

            order.ItemsTotal = order.Items.Sum(x => x.Total);

            order.OrderTotal = order.ItemsTotal + order.Freight;

            return order;
        }

        private Order loadDefaulValues(Order order)
        {
            order.DateTime = DateTime.Now;

            order.ExpirationDate = order.DateTime.AddDays(7);

            order.Status = OrderStatus.Draft;

            foreach (var orderItem in order.Items)
            {
                var item = _itemRepository.RecoverById(orderItem.Item.Id);
                
                orderItem.Price = item.Price;
            }

            return order;
        }

        private Order copyFromCurrentOrder(Order order, Order currentOrder)
        {
            order.DateTime = currentOrder.DateTime;

            order.ExpirationDate = order.ExpirationDate;

            order.Status = currentOrder.Status;

            foreach (var orderItem in order.Items)
            {
                var currentOrderItem = currentOrder.Items.First(x => x.Id == orderItem.Id);

                orderItem.Price = currentOrderItem.Price;
            }

            return order;
        }
    }
}
