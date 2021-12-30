using FluentValidation;
using FluentValidation.Results;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Orders;

namespace iParty.Business.Validations
{
    public class OrderValidation : AbstractValidator<Order>, IOrderValidation
    {
        //TODO: Implementar validações
        public ValidationResult CustomValidate(Order order)
        {
            return this.Validate(order);
        }
    }
}
