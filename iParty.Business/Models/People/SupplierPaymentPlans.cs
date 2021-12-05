using iParty.Business.Models.PaymentPlans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.People
{
    public class SupplierPaymentPlans: Entity
    {
        public Person Supplier { get; set; }
        public PaymentPlan PaymentPlan { get; set; }
    }
}
