using iParty.Business.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
