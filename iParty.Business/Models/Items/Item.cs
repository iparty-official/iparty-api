using iParty.Business.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.Items
{
    public class Item: Entity
    {
        public Person Supplier { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public object Photo { get; set; }
        public decimal Price { get; set; }
        public MeasurementUnit Unit { get; set; }
        public ProductOrService ProductOrService { get; set; }
        public Product ProductInfo { get; set; }

    }
}
