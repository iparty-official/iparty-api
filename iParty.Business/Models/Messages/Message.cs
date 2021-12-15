using iParty.Business.Models.People;
using iParty.Business.Models.Orders;
using System;

namespace iParty.Business.Models.Messages
{
    public class Message: Entity
    {
        public Person From { get; set; }
        public Person To { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }         
        public Order Order { get; set; }

    }
}
