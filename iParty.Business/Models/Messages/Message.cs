using iParty.Business.Models.People;
using iParty.Business.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.Messages
{
    public class Message: Entity
    {
        public Person From { get; set; }
        public Person To { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public Order Order { get; set; }

    }
}
