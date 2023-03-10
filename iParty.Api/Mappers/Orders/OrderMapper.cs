using AutoMapper;
using iParty.Api.Dtos.Addresses;
using iParty.Api.Dtos.Orders;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Interfaces.Mapppers;
using iParty.Api.Views.Addresses;
using iParty.Api.Views.Orders;
using iParty.Api.Views.People;
using iParty.Business.Infra.Extensions;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Services;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.Orders;
using iParty.Business.Models.PaymentPlans;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Api.Mappers.Orders
{
    public class OrderMapper : BaseMapper<Order>, IOrderMapper
    {
        private ISupplierService _supplierService;

        private ICustomerService _customerService;

        private IRepository<PaymentPlan> _paymentPlanRepository;

        private IAddressMapper _addressMapper;

        private IOrderItemMapper _orderItemMapper;

        private IMapper _autoMapper;

        public OrderMapper(ISupplierService supplierService, 
                           ICustomerService customerService, 
                           IRepository<PaymentPlan> paymentPlanRepository, 
                           IAddressMapper addressMapper, 
                           IOrderItemMapper orderItemMapper, 
                           IMapper autoMapper)
        {
            _supplierService = supplierService;
            _customerService = customerService;
            _paymentPlanRepository = paymentPlanRepository;
            _addressMapper = addressMapper;
            _orderItemMapper = orderItemMapper;
            _autoMapper = autoMapper;
        }

        public MapperResult<Order> Map(OrderDto dto)
        {
            var supplier = _supplierService.Get(dto.SupplierId).IfNull(() => { AddError("O fornecedor informado não existe."); });

            var customer = _customerService.Get(dto.CustomerId).IfNull(() => { AddError("O cliente informado não existe."); });

            var paymentPlanForOrder = mapPaymentPlan(dto);

            var shippingAddress = mapShippingAddress(dto.ShippingAddress);

            var items = new List<OrderItem>();

            if (!SuccessResult()) return GetResult();

            SetEntity(new Order(
                _autoMapper.Map<PersonForOrder>(supplier),
                _autoMapper.Map<PersonForOrder>(customer),
                shippingAddress,
                dto.Freight,
                paymentPlanForOrder,
                dto.Notes,
                dto.PartyDate,
                items)
            );
            
            return GetResult();
        }

        private PaymentPlanForOrder mapPaymentPlan(OrderDto dto)
        {
            var paymentPlan = _paymentPlanRepository.RecoverById(dto.PaymentPlanId).IfNull(() => { AddError("O plano de pagamento informado não existe."); });

            if (paymentPlan == null) return null;

            var installment = paymentPlan.Instalments.Where(x => x.Quantity == dto.Installments).FirstOrDefault();

            if (installment == null)
                AddError("O plano de pagamento informado não aceita a quantidade de parcelas que foi informada.");
           
            return new PaymentPlanForOrder(paymentPlan.Id, paymentPlan.PaymentMethod, dto.Installments, installment.Fee);

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

            var shippingAddress = order.ShippingAddress.Id;

            var paymentPlan = order.PaymentPlan.Id;

            var items = _orderItemMapper.Map(order.Items);

            var orderView = new OrderView()
            {
                Id = order.Id,
                Version = order.Version,
                DateTime = order.DateTime,
                CustomerId = order.Customer.Id,
                SupplierId = order.Supplier.Id,
                ShippingAddressId = order.ShippingAddress.Id,
                Freight = order.Freight,
                PaymentPlanFee = order.PaymentPlanFee,
                ItemsTotal = order.ItemsTotal,
                OrderTotal = order.OrderTotal,
                PaymentPlanId = order.PaymentPlan.Id,
                Notes = order.Notes,
                Status = order.Status,
                PartyDate = order.PartyDate,
                ExpirationDate = order.ExpirationDate,                
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
