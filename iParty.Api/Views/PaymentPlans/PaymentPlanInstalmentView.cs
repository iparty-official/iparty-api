namespace iParty.Api.Views.PaymentPlans
{
    public class PaymentPlanInstalmentView : View
    {
        public int Sequence { get; set; }
        public decimal Fee { get; set; }
        public decimal Prompt { get; set; }
    }
}
