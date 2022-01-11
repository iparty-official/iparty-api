﻿using iParty.Business.Models.Notifications;
using iParty.Business.Models.People;
using System;

namespace iParty.Business.Models.Notications
{
    public class Notification: Entity
    {
        public DateTime DateTime { get; set; }     
        public PersonForNotification Destination { get; set; }
        public string Text { get; set; }
    }
}
