using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Orders;

namespace iParty.Business.Validations
{
    public class OrderItemValidation : AbstractValidator<OrderItem>, IOrderItemValidation
    {
    }
}
