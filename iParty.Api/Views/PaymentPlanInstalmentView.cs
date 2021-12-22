namespace iParty.Api.Views
{
    public class PaymentPlanInstalmentView : View
    {
        public int Sequence { get; set; }
        public decimal Fee { get; set; }
        public decimal Prompt { get; set; }
    }
}
