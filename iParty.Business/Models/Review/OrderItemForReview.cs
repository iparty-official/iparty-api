using System;

namespace iParty.Business.Models.Review
{
    public class OrderItemForReview
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public ItemForOrderItemForReview Item { get; set; }
    }
}
