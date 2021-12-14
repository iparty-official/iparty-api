using iParty.Business.Models.Orders;
using iParty.Business.Models.People;
using System;

namespace iParty.Api.Views
{
    public class MessageView : View
    {
        public PersonView From { get; set; }
        public PersonView To { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        //public Order Order { get; set; }
    }
}
