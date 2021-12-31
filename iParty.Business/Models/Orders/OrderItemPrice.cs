using System;

namespace iParty.Business.Models.Orders
{
    public class OrderItemPrice
    {
        public Guid ItemId { get; set; }

        public decimal Price { get; set; }
    }
}
