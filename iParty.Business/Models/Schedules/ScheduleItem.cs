using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.Schedules
{
    public class ScheduleItem: Entity
    {
        public Schedule Schedule { get; set; }
        public int InitialHour { get; set; }
        public int FinalHour { get; set; }        
    }
}
