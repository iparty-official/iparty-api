using iParty.Business.Models.Orders;
using System;

namespace iParty.Business.Models.Messages
{
    public class Message: Entity
    {
        public Message() : base() { }
        public Message(PersonForMessage from, PersonForMessage to, string text, DateTime dateTime, Guid? orderId)
        {
            From = from;
            To = to;
            Text = text;
            DateTime = dateTime;
            OrderId = orderId;
        }
        public PersonForMessage From { get; private set; }
        public PersonForMessage To { get; private set; }
        public string Text { get; private set; }
        public DateTime DateTime { get; private set; }         
        public Guid? OrderId { get; private set; }

    }
}
