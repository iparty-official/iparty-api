using iParty.Business.Models.Orders;
using System;

namespace iParty.Business.Models.Review
{
    public class Review: Entity
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Stars { get; set; }
        public string Description { get; set; }
        public OrderItemForReview OrderItem { get; set; }
    }
}
