using iParty.Business.Models.PaymentPlans;

namespace iParty.Api.Views.Orders
{
    public class PaymentPlanForOrderView
    {
        public PaymentMethod PaymentMethod { get; set; }
        public int Installments { get; set; }
        public decimal Fee { get; set; }
    }
}
