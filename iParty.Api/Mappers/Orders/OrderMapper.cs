﻿using AutoMapper;
using iParty.Api.Dtos.Addresses;
using iParty.Api.Dtos.Orders;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Interfaces.Mapppers;
using iParty.Api.Views.Addresses;
using iParty.Api.Views.Orders;
using iParty.Api.Views.PaymentPlans;
using iParty.Api.Views.People;
using iParty.Business.Infra.Extensions;
using iParty.Business.Interfaces.Services;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.Orders;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Api.Mappers.Orders
{
    public class OrderMapper : BaseMapper<Order>, IOrderMapper
    {
        private ISupplierService _supplierService;

        private ICustomerService _customerService;

        private IAddressMapper _addressMapper;

        private IOrderItemMapper _orderItemMapper;

        private IMapper _autoMapper;        

        public OrderMapper(ISupplierService supplierService, ICustomerService customerService, IAddressMapper addressMapper, IOrderItemMapper orderItemMapper, IMapper autoMapper)
        {
            _supplierService = supplierService;
            _customerService = customerService;
            _addressMapper = addressMapper;
            _orderItemMapper = orderItemMapper;
            _autoMapper = autoMapper;           
        }

        public MapperResult<Order> Map(OrderDto dto)
        {
            var supplier = _supplierService.Get(dto.SupplierId).IfNull(() => { AddError("O fornecedor informado não existe."); });

            var customer = _customerService.Get(dto.CustomerId).IfNull(() => { AddError("O cliente informado não existe."); });            

            var shippingAddress = mapShippingAddress(dto.ShippingAddress);

            var items = mapItems(dto.Items);

            if (!SuccessResult()) return GetResult();

            SetEntity(new Order() {                
                Supplier = _autoMapper.Map<PersonForOrder>(supplier),
                Customer = _autoMapper.Map<PersonForOrder>(customer),
                ShippingAddress = shippingAddress,
                Freight = dto.Freight,
                PaymentMethod = dto.PaymentMethod,
                Installments = dto.Installments,
                Notes = dto.Notes,
                PartyDate = dto.PartyDate,
                Items = items
            });
            
            return GetResult();
        }        

        public OrderView Map(Order order)
        {
            return mapToView(order);
        }        

        public List<OrderView> Map(List<Order> orders)
        {
            var orderViewList = new List<OrderView>();

            foreach (var order in orders)
            {
                orderViewList.Add(mapToView(order));
            }

            return orderViewList;
        }

        private OrderView mapToView(Order order)
        {
            if (order == null) return null;

            var customer = _autoMapper.Map<PersonSummarizedView>(order.Customer);

            var supplier = _autoMapper.Map<PersonSummarizedView>(order.Supplier);

            var shippingAddress = _autoMapper.Map<AddressView>(order.ShippingAddress);            

            var items = _orderItemMapper.Map(order.Items);

            var orderView = new OrderView()
            {
                Id = order.Id,
                DateTime = order.DateTime,
                Customer = customer,
                Supplier = supplier,
                ShippingAddress = shippingAddress,
                Freight = order.Freight,
                ItemsTotal = order.ItemsTotal,
                OrderTotal = order.OrderTotal,
                PaymentMethod = order.PaymentMethod,
                Instalmments = order.Installments,
                Notes = order.Notes,
                Status = order.Status,
                PartyDate = order.PartyDate,
                ExpirationDate = order.ExpirationDate,
                Items = items
            };

            return orderView;
        }

        private List<OrderItem> mapItems(List<OrderItemDto> items)
        {
            var resultList = _orderItemMapper.Map(items);

            if (resultList.Exists(x => !x.Success))
            {
                foreach (var result in resultList)
                {
                    foreach (var erro in result.Errors) AddError(erro);                                            
                }                    
                    
                return new List<OrderItem>();
            }
            else
            {
                return resultList.Select(x => x.Entity).ToList();
            }
        }        

        private Address mapShippingAddress(AddressDto shippingAddress)
        {
            var result = _addressMapper.Map(shippingAddress);

            if (result.Success)
                return result.Entity;
            else
            {
                foreach (var erro in result.Errors) AddError(erro);

                return null;
            }
        }
    }
}
