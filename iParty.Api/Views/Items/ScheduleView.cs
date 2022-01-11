using System;
using System.Collections.Generic;

namespace iParty.Api.Views.Items
{
    public class ScheduleView : View
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<ScheduleItemView> Items { get; set; }
    }
}
