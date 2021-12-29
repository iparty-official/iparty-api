using iParty.Business.Models.Items;
using System;
using System.Collections.Generic;

namespace iParty.Api.Dtos.Items
{
    public class ItemDto
    {
        public Guid SupplierId { get; set; }        
        public string SKU { get; set; }
        public string Name { get; set; }        
        public string Details { get; set; }        
        public object Photo { get; set; }        
        public decimal Price { get; set; }        
        public MeasurementUnit Unit { get; set; }        
        public ProductOrService ProductOrService { get; set; }        
        public RentOrSale ForRentOrSale { get; set; }        
        public int AvailableQuantity { get; set; }
        public List<ScheduleDto> Schedules { get; set; }
    }
}
