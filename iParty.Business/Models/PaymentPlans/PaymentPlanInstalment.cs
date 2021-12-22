namespace iParty.Business.Models.PaymentPlans
{
    public class PaymentPlanInstalment: Entity
    {
        public int Sequence { get; set; }
        public decimal Fee { get; set; }
        public int Prompt { get; set; }
    }
}