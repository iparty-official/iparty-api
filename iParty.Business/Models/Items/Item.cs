using iParty.Business.Models.People;
using System;
using System.Collections.Generic;

namespace iParty.Business.Models.Items
{
    public class Item: Entity
    {
        public Person Supplier { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public object Photo { get; set; }
        public decimal Price { get; set; }
        public MeasurementUnit Unit { get; set; }
        public ProductOrService ProductOrService { get; set; }
        public Product ProductInfo { get; set; }
        public List<Schedule> Schedules { get; set; }
        
        public void ReplaceSchedule(Guid scheduleId, Schedule newSchedule)
        {
            var currentSchedule = Schedules.Find(x => x.Id == scheduleId);

            if (currentSchedule == null)
                throw new Exception("Não foi possível localizar a agenda informada");

            var index = Schedules.IndexOf(currentSchedule);

            Schedules.Remove(currentSchedule);

            newSchedule.Id = scheduleId;

            Schedules.Insert(index, newSchedule);            
        }

        public void RemoveSchedule(Guid scheduleId)
        {
            var currentSchedule = Schedules.Find(x => x.Id == scheduleId);

            if (currentSchedule == null)
                throw new Exception("Não foi possível localizar a agenda informada");

            var index = Schedules.IndexOf(currentSchedule);

            Schedules.Remove(currentSchedule);            
        }
    }
}
