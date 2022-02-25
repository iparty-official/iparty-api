using System;

namespace iParty.Business.Models.Review
{
    public class Review: Entity
    {
        public Review() : base() { }
        public Review(DateTime dateTime, int stars, string description, OrderItemForReview orderItem)
        {
            DateTime = dateTime;
            Stars = stars;
            Description = description;
            OrderItem = orderItem;
        }
        public DateTime DateTime { get; private set; }       
        public int Stars { get; private set; }
        public string Description { get; private set; }
        public OrderItemForReview OrderItem { get; private set; }
    }
}
