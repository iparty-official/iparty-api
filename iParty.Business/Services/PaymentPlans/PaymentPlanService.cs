using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.PaymentPlans;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Services.PaymentPlans
{
    public class PaymentPlanService : Service<PaymentPlan, IRepository<PaymentPlan>>, IPaymentPlanService
    {
        private IPaymentPlanValidation _paymentPlanValidation;

        public PaymentPlanService(IRepository<PaymentPlan> rep, IPaymentPlanValidation paymentPlanValidation) : base(rep)
        {
            _paymentPlanValidation = paymentPlanValidation;
        }

        public ServiceResult<PaymentPlan> Create(PaymentPlan paymentPlan)
        {
            var result = ExecuteValidation(_paymentPlanValidation, paymentPlan);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(paymentPlan);

            return GetSuccessResult(paymentPlan);
        }

        public ServiceResult<PaymentPlan> Update(Guid id, PaymentPlan paymentPlan)
        {
            var currentPaymentPlan = Get(id);

            if (currentPaymentPlan == null)
                return GetFailureResult("Não foi possível localizar a forma de pagamento informada.");

            var result = ExecuteValidation(_paymentPlanValidation, paymentPlan);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, paymentPlan);

            return GetSuccessResult(currentPaymentPlan);
        }
    }
}
