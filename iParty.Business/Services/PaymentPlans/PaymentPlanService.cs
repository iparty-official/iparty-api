using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Interfaces;
using System;
using System.Collections.Generic;

namespace iParty.Business.Services.PaymentPlans
{
    public class PaymentPlanService : IPaymentPlanService
    {
        private BasicService<PaymentPlan> _basicService;

        private IPaymentPlanValidation _paymentPlanValidation;

        public PaymentPlanService(IRepository<PaymentPlan> repository, IPaymentPlanValidation paymentPlanValidation)
        {
            _paymentPlanValidation = paymentPlanValidation;
            _basicService = new BasicService<PaymentPlan>(repository, paymentPlanValidation);
        }

        public ServiceResult<PaymentPlan> Create(PaymentPlan paymentPlan)
        {
            return _basicService.Create(paymentPlan);
        }

        public ServiceResult<PaymentPlan> Update(Guid id, PaymentPlan paymentPlan)
        {
            return _basicService.Update(id, paymentPlan);
        }

        public ServiceResult<PaymentPlan> Delete(Guid id)
        {
            return _basicService.Delete(id);
        }

        public PaymentPlan Get(Guid id)
        {
            return _basicService.Get(id);
        }

        public List<PaymentPlan> Get()
        {
            return _basicService.Get();
        }
    }
}
