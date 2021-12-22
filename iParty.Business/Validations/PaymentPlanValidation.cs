using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.PaymentPlans;

namespace iParty.Business.Validations
{
    public class PaymentPlanValidation : AbstractValidator<PaymentPlan>, IPaymentPlanValidation
    {

        public PaymentPlanValidation()
        {
            RuleFor(p => p.Description).NotEmpty().WithMessage("A descrição da forma de pagamento não foi informada.");

            RuleFor(p => p.PaymentMethod).IsInEnum().WithMessage("O campo 'Método de pagamento' está com um valor inválido.");

            RuleFor(p => p.MinInstallmentValue).GreaterThanOrEqualTo(0).WithMessage("O campo 'Valor mínimo da prestação' não pode ser negativo.");

            RuleForEach(p => p.Instalments).ChildRules(instalment => instalment.RuleFor(x => x.Sequence).GreaterThan(0).WithMessage("A sequência informada na prestação é inválida."));
            
            RuleForEach(p => p.Instalments).ChildRules(instalment => instalment.RuleFor(x => x.Fee).GreaterThanOrEqualTo(0).WithMessage("A taxa informada na prestação não pode ser negativa."));

            RuleForEach(p => p.Instalments).ChildRules(instalment => instalment.RuleFor(x => x.Prompt).GreaterThanOrEqualTo(0).WithMessage("O prazo informado na prestação não pode ser negativo."));
        }
    }
}
