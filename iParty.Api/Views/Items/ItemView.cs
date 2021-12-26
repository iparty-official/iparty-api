using iParty.Api.Views.People;
using iParty.Business.Models.Items;
using System.Collections.Generic;

namespace iParty.Api.Views.Items
{
    public class ItemView : View
    {
        public PersonSummarizedView Supplier { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public object Photo { get; set; }
        public decimal Price { get; set; }
        public MeasurementUnit Unit { get; set; }
        public ProductOrService ProductOrService { get; set; }
        public decimal AvailableQuantity { get; set; }
        public RentOrSale ForRentOrSale { get; set; }
        public List<ScheduleView> Schedules { get; set; }
    }
}
