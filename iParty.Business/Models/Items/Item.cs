using iParty.Business.Models.People;
using System;
using System.Collections.Generic;

namespace iParty.Business.Models.Items
{
    public class Item: Entity
    {
        public Item() : base() { }
        public Item(PersonForItem supplier, string sku, string name, string details, object photo, decimal price, MeasurementUnit unit, ProductOrService productOrService, Product productInfo, List<Schedule> schedules)
        {
            Supplier = supplier;
            SKU = sku;
            Name = name;
            Details = details;
            Photo = photo;
            Price = price;
            Unit = unit;
            ProductOrService = productOrService;
            ProductInfo = productInfo;
            Schedules = schedules;
        }
        public PersonForItem Supplier { get; private set; }
        public string SKU { get; private set; }
        public string Name { get; private set; }
        public string Details { get; private set; }
        public object Photo { get; private set; }
        public decimal Price { get; private set; }
        public MeasurementUnit Unit { get; private set; }
        public ProductOrService ProductOrService { get; private set; }
        public Product ProductInfo { get; private set; }
        public List<Schedule> Schedules { get; private set; }        
        public void ReplaceSchedule(Guid scheduleId, Schedule newSchedule)
        {
            var currentSchedule = Schedules.Find(x => x.Id == scheduleId);

            if (currentSchedule == null)
                throw new Exception("Não foi possível localizar a agenda informada");

            var index = Schedules.IndexOf(currentSchedule);

            Schedules.Remove(currentSchedule);

            newSchedule.DefineIdAndVersion(scheduleId, Guid.NewGuid());

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
