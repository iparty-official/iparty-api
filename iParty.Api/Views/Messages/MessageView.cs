using iParty.Api.Views.Orders;
using iParty.Api.Views.People;
using System;

namespace iParty.Api.Views.Messages
{
    public class MessageView : View
    {
        public PersonSummarizedView From { get; set; }
        public PersonSummarizedView To { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public OrderView Order { get; set; }
    }
}
