using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.PaymentPlans;

namespace iParty.Business.Validations
{
    public class PaymentPlanValidation : AbstractValidator<PaymentPlan>, IPaymentPlanValidation
    {
        public PaymentPlanValidation()
        {
            RuleFor(p => p.PaymentMethod).IsInEnum().WithMessage("O campo 'Método de pagamento' está com um valor inválido.");

            RuleFor(p => p.MinInstallmentValue).GreaterThanOrEqualTo(0).WithMessage("O campo 'Valor mínimo da prestação' não pode ser negativo.");
            
            RuleForEach(p => p.Instalments).ChildRules(instalment => instalment.RuleFor(x => x.Fee).GreaterThanOrEqualTo(0).WithMessage("A taxa informada na prestação não pode ser negativa."));
        }
    }
}
