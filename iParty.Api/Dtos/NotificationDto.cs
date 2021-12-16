using System;

namespace iParty.Api.Dtos
{
    public class NotificationDto
    {
        public Guid DestinationId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }        
        public string Text { get; set; }
    }
}
