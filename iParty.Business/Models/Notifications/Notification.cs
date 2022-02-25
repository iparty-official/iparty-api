using iParty.Business.Models.Notifications;
using System;

namespace iParty.Business.Models.Notications
{
    public class Notification: Entity
    {
        public Notification() : base() { }
        public Notification(DateTime dateTime, PersonForNotification destination, string text)
        {
            DateTime = dateTime;
            Destination = destination;
            Text = text;
        }
        public DateTime DateTime { get; private set; }     
        public PersonForNotification Destination { get; private set; }
        public string Text { get; private set; }
    }
}
