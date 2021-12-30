using FluentValidation;
using iParty.Business.Models.Orders;

namespace iParty.Business.Interfaces.Validations
{
    public interface IOrderItemValidation : IValidator<OrderItem>
    {        
    }
}
