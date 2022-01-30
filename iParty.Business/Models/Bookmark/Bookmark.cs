using iParty.Business.Models.Items;
using iParty.Business.Models.People;
using System;

namespace iParty.Business.Models.Bookmark
{
    public class Bookmark: Entity
    {
        public Bookmark() { }
        public Bookmark(Person customer, Item item, DateTime dateTime)
        {
            Customer = customer;
            Item = item;
            DateTime = dateTime;
        }
        public Person Customer { get; private set; }
        public Item Item{ get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
