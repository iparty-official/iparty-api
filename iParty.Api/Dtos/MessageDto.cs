using iParty.Business.Models.Orders;
using iParty.Business.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParty.Api.Dtos
{
    public class MessageDto
    {
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public Guid OrderId { get; set; }
    }
}
