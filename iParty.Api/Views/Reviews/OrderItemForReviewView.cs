using System;

namespace iParty.Api.Views.Reviews
{
    public class OrderItemForReviewView : View
    {
        public Guid OrderId { get; set; }        
        public ItemForOrderItemForReviewView Item { get; set; }
    }
}
