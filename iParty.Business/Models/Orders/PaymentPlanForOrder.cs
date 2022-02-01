using iParty.Business.Models.PaymentPlans;

namespace iParty.Business.Models.Orders
{
    public class PaymentPlanForOrder : Entity
    {
        public PaymentPlanForOrder() { }
        public PaymentPlanForOrder(PaymentMethod paymentMethod, int installments, decimal fee)
        {
            PaymentMethod = paymentMethod;
            Installments = installments;
            Fee = fee;
        }
        public PaymentMethod PaymentMethod { get; private set; }        
        public int Installments { get; private set; }
        public decimal Fee { get; private set; }
    }
}
