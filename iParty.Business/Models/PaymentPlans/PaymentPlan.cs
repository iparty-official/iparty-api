using System.Collections.Generic;

namespace iParty.Business.Models.PaymentPlans
{
    public class PaymentPlan: Entity
    {              
        public PaymentMethod PaymentMethod { get; set; }
        
        public decimal MinInstallmentValue { get; set; }

        public List<PaymentPlanInstalment> Instalments { get; set; }
    }
}
