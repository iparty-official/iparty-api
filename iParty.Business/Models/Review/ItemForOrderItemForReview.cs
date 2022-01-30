using System;

namespace iParty.Business.Models.Review
{
    public class ItemForOrderItemForReview
    {
        public ItemForOrderItemForReview() { }
        public ItemForOrderItemForReview(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
