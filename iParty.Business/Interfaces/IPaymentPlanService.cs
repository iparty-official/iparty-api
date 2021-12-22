using iParty.Business.Infra;
using iParty.Business.Models.PaymentPlans;
using System;

namespace iParty.Business.Interfaces
{
    public interface IPaymentPlanService : IService<PaymentPlan>
    {
        ServiceResult<PaymentPlan> Create(PaymentPlan paymentPlan);
        ServiceResult<PaymentPlan> Update(Guid id, PaymentPlan paymentPlan);
    }
}
