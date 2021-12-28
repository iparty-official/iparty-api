using AutoMapper;
using iParty.Api.Dtos.Orders;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Interfaces.Mapppers;
using iParty.Api.Views.Addresses;
using iParty.Api.Views.Orders;
using iParty.Api.Views.People;
using iParty.Business.Infra.Extensions;
using iParty.Business.Interfaces.Services;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.Orders;
using iParty.Business.Models.PaymentPlans;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Api.Mappers.Orders
{
    public class OrderMapper : BaseMapper<Order>, IOrderMapper
    {
        //TODO: Devo injetar serviços no lugar de repositórios
        //TODO: Criar método GET no serviço que receba um FilerBuilder
        //TODO: Rever mappers
        
        private IMapper _autoMapper;

        private IAddressMapper _addressMapper;

        private IOrderItemMapper _orderItemMapper;

        private ICustomerService _customerService;

        private ISupplierService _supplierService;

        private IRepository<PaymentPlan> _paymentPlanRepository;

        public OrderMapper(IMapper autoMapper, 
                           ICustomerService customerService, 
                           ISupplierService supplierService, 
                           IAddressMapper addressMapper,                           
                           IOrderItemMapper orderItemMapper,
                           IRepository<PaymentPlan> paymentPlanRepository)
        {
            _autoMapper = autoMapper;
            _customerService = customerService;
            _supplierService = supplierService;
            _addressMapper = addressMapper;
            _paymentPlanRepository = paymentPlanRepository;
            _orderItemMapper = orderItemMapper;
        }

        public MapperResult<Order> Map(OrderDto dto)
        {
            var customer = _customerService.Get(dto.CustomerId).IfNull(() => AddError("O cliente informado não foi encontrado."));

            var supplier = _supplierService.Get(dto.CustomerId).IfNull(() => AddError("O fornecedor informado não foi encontrado."));

            var paymentPlan = _paymentPlanRepository.RecoverById(dto.PaymentPlanId).IfNull(() => AddError("O plano de pagamento informado não foi encontrado."));

            if (!SuccessResult()) return GetResult();

            var order = new Order()
            {
                
                DateTime = DateTime.Now,
                Supplier = supplier,
                Customer = customer,
                ShippingAddress = new Address(),
                Freight = dto.Freight,
                PaymentPlan = paymentPlan,
                Notes = dto.Notes,
                PartyDate = dto.PartyDate,
                Items = new List<OrderItem>()                                                                
            };

            var mapperResult = _addressMapper.Map(dto.ShippingAddress);

            if (!mapperResult.Success)
                foreach (var erro in mapperResult.Errors) AddError(erro);

            if (!SuccessResult()) return GetResult();                        

            order.Items.AddRange(dto.Items.Select(x =>
            {
                var mapperResult = _orderItemMapper.Map(x);

                if (!mapperResult.Success)
                    foreach (var erro in mapperResult.Errors) AddError(erro);

                return mapperResult.Entity;
            }));

            if (!SuccessResult()) return GetResult();

            SetEntity(order);

            return GetResult();
        }

        public OrderView Map(Order order)
        {
            return mapOrderToOrderView(order);
        }

        public List<OrderView> Map(List<Order> orders)
        {
            var result = new List<OrderView>();

            foreach (var order in orders)
            {
                result.Add(mapOrderToOrderView(order));
            }

            return result;
        }

        private OrderView mapOrderToOrderView(Order order)
        {           
            if (order == null) return null;

            var orderView = new OrderView()
            {
                Id = order.Id               
            };            

            return orderView;
        }

    }
}
