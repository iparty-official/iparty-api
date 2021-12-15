using System;

namespace iParty.Api.Views
{
    public class MessageView : View
    {
        public PersonView From { get; set; }
        public PersonView To { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public OrderView Order { get; set; }
    }
}
