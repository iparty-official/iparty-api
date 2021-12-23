using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParty.Api.Views.Items
{
    public class ScheduleItemView : View
    {
        public int InitialHour { get; set; }        
        public int FinalHour { get; set; }
    }
}
