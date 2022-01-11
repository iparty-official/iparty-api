using iParty.Api.Views.Orders;
using System;

namespace iParty.Api.Views.Reviews
{
    public class ReviewView : View
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Stars { get; set; }
        public string Description { get; set; }
        public OrderItemForReviewView OrderItem { get; set; }
    }
}
