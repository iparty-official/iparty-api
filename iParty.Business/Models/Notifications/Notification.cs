using iParty.Business.Models.People;
using System;

namespace iParty.Business.Models.Notications
{
    public class Notification: Entity
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public Person Destination { get; set; }
        public string Text { get; set; }
    }
}
