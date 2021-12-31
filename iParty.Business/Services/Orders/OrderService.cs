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

        private IRepository<Item> _itemRepository;

        public OrderService(IRepository<Order> rep, IOrderValidation orderValidation, IOrderItemValidation orderItemValidation, IRepository<Item> itemRepository) : base(rep)
        {
            _orderValidation = orderValidation;
            _orderItemValidation = orderItemValidation;
            _itemRepository = itemRepository;
        }

        public ServiceResult<Order> Create(Order order)
        {
            //TODO: Calcular taxa do plano de pagamento.
            
            order.SetDefaultValuesForNewOrder(getItemsPrices(order));            

            order.TotalizeOrder();

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

            order.SetDefaultValuesForUpdatedOrder(currentOrder, getItemsPrices(currentOrder));

            order.TotalizeOrder();

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

        private List<OrderItemPrice> getItemsPrices(Order order)
        {
            var prices = new List<OrderItemPrice>();

            foreach (var item in order.Items)
            {
                prices.Add(new OrderItemPrice()
                {
                    ItemId = item.Id,
                    Price = _itemRepository.RecoverById(item.Id).Price
                });
            }

            return prices;
        }
    }
}
