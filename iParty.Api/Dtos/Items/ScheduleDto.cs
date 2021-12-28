using System;
using System.Collections.Generic;

namespace iParty.Api.Dtos.Items
{
    public class ScheduleDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<ScheduleItemDto> Items { get; set; }
    }
}
