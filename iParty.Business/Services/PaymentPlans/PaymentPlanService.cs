using iParty.Business.Infra;
using iParty.Business.Interfaces;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Validations;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Services.PaymentPlans
{
    public class PaymentPlanService : Service<PaymentPlan, IRepository<PaymentPlan>>, IPaymentPlanService
    {
        public PaymentPlanService(IRepository<PaymentPlan> rep) : base(rep)
        {
        }

        public ServiceResult<PaymentPlan> Create(PaymentPlan paymentPlan)
        {
            var result = ExecuteValidation(new PaymentPlanValidation(), paymentPlan);

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

            var result = ExecuteValidation(new PaymentPlanValidation(), paymentPlan);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, paymentPlan);

            return GetSuccessResult(currentPaymentPlan);
        }
    }
}
