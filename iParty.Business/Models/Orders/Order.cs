using iParty.Business.Models.Addresses;
using iParty.Business.Models.PaymentPlans;
using System;
using System.Collections.Generic;

namespace iParty.Business.Models.Orders
{
    public class Order: Entity
    {
        public DateTime DateTime { get; set; }
        public PersonForOrder Supplier { get; set; }
        public PersonForOrder Customer { get; set; }
        public Address ShippingAddress { get; set; }
        public decimal Freight { get; set; }
        public decimal ItemsTotal { get; set; }
        public decimal OrderTotal { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int Installments { get; set; }
        public string Notes { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime PartyDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
