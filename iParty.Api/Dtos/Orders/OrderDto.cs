using iParty.Api.Dtos.Addresses;
using iParty.Business.Models.PaymentPlans;
using System;
using System.Collections.Generic;

namespace iParty.Api.Dtos.Orders
{
    public class OrderDto
    {        
        public Guid SupplierId { get; set; }
        public Guid CustomerId { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public decimal Freight { get; set; }
        public Guid PaymentPlanId { get; set; }
        public int Installments { get; set; }
        public string Notes { get; set; }
        public DateTime PartyDate { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
