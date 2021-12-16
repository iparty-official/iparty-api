using System;

namespace iParty.Api.Views
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
