using iParty.Api.Views.Orders;
using iParty.Api.Views.People;
using System;

namespace iParty.Api.Views.Messages
{
    public class MessageView : View
    {
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public Guid OrderId { get; set; }
    }
}
