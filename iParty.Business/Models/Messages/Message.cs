using iParty.Business.Models.Orders;
using System;

namespace iParty.Business.Models.Messages
{
    public class Message: Entity
    {
        public Message() { }
        public Message(PersonForMessage from, PersonForMessage to, string text, DateTime dateTime, Order order)
        {
            From = from;
            To = to;
            Text = text;
            DateTime = dateTime;
            Order = order;
        }
        public PersonForMessage From { get; private set; }
        public PersonForMessage To { get; private set; }
        public string Text { get; private set; }
        public DateTime DateTime { get; private set; }         
        public Order Order { get; private set; }

    }
}
