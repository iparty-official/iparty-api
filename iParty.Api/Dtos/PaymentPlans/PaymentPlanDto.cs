using iParty.Business.Models.PaymentPlans;
using System.Collections.Generic;

namespace iParty.Api.Dtos.PaymentPlans
{
    public class PaymentPlanDto
    {
        public PaymentMethod PaymentMethod { get; set; }

        public decimal MinInstallmentValue { get; set; }

        public List<PaymentPlanInstalmentDto> Instalments { get; set; }
    }
}
