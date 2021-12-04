using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.PaymentPlans
{
    public class PaymentPlan: Entity
    {
        public PaymentMethod PaymentMethod { get; set; }
        public decimal MinInstallmentValue { get; set; }
        public List<PaymentPlanInstalments> Instalments { get; set; }
    }
}
