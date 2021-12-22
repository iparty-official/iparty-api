using iParty.Business.Models.PaymentPlans;
using System.Collections.Generic;

namespace iParty.Api.Views.PaymentPlans
{
    public class PaymentPlanView : View
    {
        public PaymentMethod PaymentMethod { get; set; }

        public decimal MinInstallmentValue { get; set; }

        public List<PaymentPlanInstalmentView> Instalments { get; set; }
    }
}
