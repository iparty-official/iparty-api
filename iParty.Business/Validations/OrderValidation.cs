using FluentValidation;
using FluentValidation.Results;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Orders;
using iParty.Business.Models.PaymentPlans;
using System;

namespace iParty.Business.Validations
{
    public class OrderValidation : AbstractValidator<Order>, IOrderValidation
    {
        private IOrderItemValidation _orderItemValidation;

        private IAddressValidation _addressValidation;

        public OrderValidation(ICustomerService customerService, 
                               ISupplierService supplierService, 
                               IRepository<PaymentPlan> paymentPlanRepository,
                               IOrderItemValidation orderItemValidation,
                               IAddressValidation addressValidation)
        {
            _orderItemValidation = orderItemValidation;
            
            _addressValidation = addressValidation;

            RuleFor(x => x.DateTime).GreaterThan(DateTime.MinValue).WithMessage("A data do pedido não foi informada.");

            RuleFor(x => x.DateTime).LessThanOrEqualTo(DateTime.Now).WithMessage("A data do pedido não pode ser maior que a data atual.");

            RuleFor(x => x.Supplier).NotNull().WithMessage("O fornecedor do pedido não foi informado.");
            
            RuleFor(x => customerService.Get(x.Supplier.Id)).NotNull().WithMessage("O fornecedor informado não foi encontrado.");

            RuleFor(x => x.Customer).NotNull().WithMessage("O cliente do pedido não foi informado.");

            RuleFor(x => supplierService.Get(x.Customer.Id)).NotNull().WithMessage("O cliente informado não foi encontrado.");

            RuleFor(x => x.ShippingAddress).NotNull().WithMessage("O endereço do pedido não foi informado.");

            RuleFor(x => x.Freight).GreaterThanOrEqualTo(0).WithMessage("O valor do frete não pode ser menor que zero.");

            RuleFor(x => x.ItemsTotal).GreaterThan(0).WithMessage("O valor total dos items deve ser maior que zero.");

            RuleFor(x => x.ItemsTotal).Equal(x => x.CalculateItemsTotal()).WithMessage("O valor total dos items deve ser maior que zero.");

            RuleFor(x => x.OrderTotal).GreaterThan(0).WithMessage("O valor total do pedido deve ser maior que zero.");

            RuleFor(x => x.OrderTotal).Equal(x => x.CalculateOrderTotal()).WithMessage("O valor total do pedido é inválido.");

            RuleFor(x => x.PaymentPlanFee).Equal(x => x.CalculatePaymentPlanFee()).WithMessage("O valor da taxa do plano de pagamento é inválida.");

            RuleFor(x => x.PaymentPlan).NotNull().WithMessage("O plano de pagamento não foi informado.");

            RuleFor(x => paymentPlanRepository.RecoverById(x.PaymentPlan.Id)).NotNull().WithMessage("O plano de pagamento informado não foi encontrado.");

            RuleFor(x => x.PaymentPlan.PaymentMethod).IsInEnum().WithMessage("O valor informado no campo 'Método de pagamento' á inválido.");

            RuleFor(x => x.PaymentPlan.Installments).GreaterThan(0).WithMessage("A quantidade de parcelas deve ser maior que zero.");

            RuleFor(x => x.Status).IsInEnum().WithMessage("O valor do campo 'Status' á inválido.");

            RuleFor(x => x.PartyDate).GreaterThan(DateTime.Now).WithMessage("A data/hora da festa precisa ser maior que a data/hora atual.");

            RuleFor(x => x.ExpirationDate).Equal(x => x.CalculateExpirationDate(x.DateTime)).WithMessage("A data/hora de expiração do pedido é inválida.");

            RuleFor(x => x.Items.Count).GreaterThan(0).WithMessage("O pedido precisa ter ao menos um item.");
        }

        public ValidationResult CustomValidate(Order order)
        {
            var result = this.Validate(order);

            if (!result.IsValid) return result;

            result = _addressValidation.Validate(order.ShippingAddress);

            if (!result.IsValid) return result;

            foreach (var item in order.Items)
            {
                result = _orderItemValidation.Validate(item);

                if (!result.IsValid) return result;
            }

            return result;
        }
    }
}
