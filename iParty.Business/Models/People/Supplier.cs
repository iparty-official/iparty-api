using iParty.Business.Models.PaymentPlans;
using System.Collections.Generic;

namespace iParty.Business.Models.People
{
    public class Supplier
    {
        public Supplier() {}
        public Supplier(string businessDescription, List<PaymentPlan> paymentPlans)
        {
            BusinessDescription = businessDescription;
            PaymentPlans = paymentPlans;
        }
        public string BusinessDescription { get; private set; }
        public List<PaymentPlan> PaymentPlans { get; private set; }
    }
}
