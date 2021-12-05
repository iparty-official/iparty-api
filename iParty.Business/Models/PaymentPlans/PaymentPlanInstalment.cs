namespace iParty.Business.Models.PaymentPlans
{
    public class PaymentPlanInstalment: Entity
    {
        public PaymentPlan PaymentPlan { get; set; }
        public int Quantity { get; set; }
        public decimal Fee { get; set; }
    }
}