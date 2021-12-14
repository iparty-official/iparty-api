using iParty.Business.Models.Orders;
using iParty.Business.Models.People;
using System;

namespace iParty.Api.Views
{
    public class MessageView : View
    {
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public Guid OrderId { get; set; }
    }
}
