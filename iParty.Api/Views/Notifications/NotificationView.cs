﻿using iParty.Api.Views.People;
using System;

namespace iParty.Api.Views.Notifications
{
    public class NotificationView : View
    {
        public DateTime DateTime { get; set; }
        public Guid DestinationId { get; set; }
        public string Text { get; set; }
    }
}
