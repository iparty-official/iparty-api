using FluentValidation;
using iParty.Business.Models.PaymentPlans;

namespace iParty.Business.Interfaces.Validations
{
    public interface IPaymentPlanValidation : IValidator<PaymentPlan>
    {
    }
}
