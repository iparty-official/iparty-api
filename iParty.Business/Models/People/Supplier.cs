using iParty.Business.Models.PaymentPlans;
using System.Collections.Generic;

namespace iParty.Business.Models.People
{
    public class Supplier
    {
        public string BusinessDescription { get; set; }

        public List<PaymentPlan> PaymentPlans { get; set; }
    }
}
