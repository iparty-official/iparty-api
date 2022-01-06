using iParty.Api.Views.Addresses;
using iParty.Api.Views.PaymentPlans;
using iParty.Api.Views.People;
using iParty.Business.Models.Orders;
using iParty.Business.Models.PaymentPlans;
using System;
using System.Collections.Generic;

namespace iParty.Api.Views.Orders
{
    public class OrderView : View
    {
        public DateTime DateTime { get; set; }
        public PersonSummarizedView Supplier { get; set; }
        public PersonSummarizedView Customer { get; set; }
        public AddressView ShippingAddress { get; set; }
        public decimal Freight { get; set; }        
        public decimal PaymentPlanFee { get; set; }
        public decimal ItemsTotal { get; set; }
        public decimal OrderTotal { get; set; }
        public PaymentPlanForOrderView PaymentPlan { get; set; }        
        public string Notes { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime PartyDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<OrderItemView> Items { get; set; }
    }
}
