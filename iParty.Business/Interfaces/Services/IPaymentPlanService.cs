using iParty.Business.Infra;
using iParty.Business.Models.PaymentPlans;
using System;

namespace iParty.Business.Interfaces.Services
{
    public interface IPaymentPlanService : IService<PaymentPlan>
    {
        ServiceResult<PaymentPlan> Create(PaymentPlan paymentPlan);
        ServiceResult<PaymentPlan> Update(Guid id, PaymentPlan paymentPlan);
    }
}
