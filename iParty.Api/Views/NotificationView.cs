using System;

namespace iParty.Api.Views
{
    public class NotificationView : View
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public PersonSummarizedView Destination { get; set; }
        public string Text { get; set; }
    }
}
