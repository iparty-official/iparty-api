using System;
using System.Collections.Generic;

namespace iParty.Business.Models.Items
{
    public class Schedule : Entity
    {
        public DayOfWeek DayOfWeek{ get; set; }
        public List<ScheduleItem> Items { get; set; }        
    }
}
