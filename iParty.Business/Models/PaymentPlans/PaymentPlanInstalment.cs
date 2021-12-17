namespace iParty.Business.Models.PaymentPlans
{
    public class PaymentPlanInstalment: Entity
    {        
        public int Quantity { get; set; }
        public decimal Fee { get; set; }
    }
}