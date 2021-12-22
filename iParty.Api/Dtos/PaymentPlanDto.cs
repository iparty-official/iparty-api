using iParty.Business.Models.PaymentPlans;
using System.Collections.Generic;

namespace iParty.Api.Dtos
{
    public class PaymentPlanDto
    {
        public string Description { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public decimal MinInstallmentValue { get; set; }

        public List<PaymentPlanInstalmentDto> Instalments { get; set; }
    }
}
