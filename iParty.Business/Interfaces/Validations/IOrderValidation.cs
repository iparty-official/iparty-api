using FluentValidation;
using FluentValidation.Results;
using iParty.Business.Models.Orders;

namespace iParty.Business.Interfaces.Validations
{
    public interface IOrderValidation : IValidator<Order>
    {
        public ValidationResult CustomValidate(Order order);
    }
}
