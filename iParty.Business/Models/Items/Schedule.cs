using System;
using System.Collections.Generic;

namespace iParty.Business.Models.Items
{
    public class Schedule : Entity
    {
        public Schedule() : base() { }
        public Schedule(DayOfWeek dayOfWeek, List<ScheduleItem> items)
        {
            DayOfWeek = dayOfWeek;
            Items = items;
        }
        public DayOfWeek DayOfWeek{ get; private set; }
        public List<ScheduleItem> Items { get; private set; }        
    }
}
