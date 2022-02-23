using System;

namespace iParty.Business.Models.Bookmark
{
    public class Bookmark: Entity
    {
        public Bookmark() : base() { } 
        public Bookmark(PersonForBookmark customer, ItemForBookmark item, DateTime dateTime)
        {
            Customer = customer;
            Item = item;
            DateTime = dateTime;
        }
        public PersonForBookmark Customer { get; private set; }
        public ItemForBookmark Item { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
