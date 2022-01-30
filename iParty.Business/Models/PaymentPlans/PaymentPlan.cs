using System.Collections.Generic;

namespace iParty.Business.Models.PaymentPlans
{
    public class PaymentPlan: Entity
    {
        public PaymentPlan() { }
        public PaymentPlan(PaymentMethod paymentMethod, decimal minInstallmentValue, List<PaymentPlanInstalment> instalments)
        {
            PaymentMethod = paymentMethod;
            MinInstallmentValue = minInstallmentValue;
            Instalments = instalments;
        }
        public PaymentMethod PaymentMethod { get; private set; }        
        public decimal MinInstallmentValue { get; private set; }
        public List<PaymentPlanInstalment> Instalments { get; private set; }
    }
}
