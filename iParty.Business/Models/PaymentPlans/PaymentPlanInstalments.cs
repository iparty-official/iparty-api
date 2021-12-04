namespace iParty.Business.Models.PaymentPlans
{
    public class PaymentPlanInstalments: Entity
    {
        public int Quantity { get; set; }
        public decimal Fee { get; set; }
    }
}