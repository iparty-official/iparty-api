using iParty.Business.Models.PaymentPlans;

namespace iParty.Business.Models.Orders
{
    public class PaymentPlanForOrder : Entity
    {
        public PaymentMethod PaymentMethod { get; set; }        
        public int Installments { get; set; }
        public decimal Fee { get; set; }
    }
}
