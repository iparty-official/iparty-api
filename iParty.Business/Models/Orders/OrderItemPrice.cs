using System;

namespace iParty.Business.Models.Orders
{
    public class OrderItemPrice
    {
        public Guid OrderItemId { get; set; }

        public decimal Price { get; set; }
    }
}
