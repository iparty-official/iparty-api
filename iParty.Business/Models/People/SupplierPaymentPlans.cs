using iParty.Business.Models.PaymentPlans;

namespace iParty.Business.Models.People
{
    public class SupplierPaymentPlans: Entity
    {
        public Person Supplier { get; set; }
        public PaymentPlan PaymentPlan { get; set; }
    }
}
