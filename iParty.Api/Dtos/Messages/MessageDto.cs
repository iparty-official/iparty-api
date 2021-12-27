using System;

namespace iParty.Api.Dtos.Messages
{
    public class MessageDto
    {
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public string Text { get; set; }        
        public Guid? OrderId { get; set; }
    }
}
