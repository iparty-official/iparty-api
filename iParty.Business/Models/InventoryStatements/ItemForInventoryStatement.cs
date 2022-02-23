using iParty.Business.Models.Items;
using System;

namespace iParty.Business.Models.InventoryStatements
{
    public class ItemForInventoryStatement
    {
        public ItemForInventoryStatement()
        {
        }
        public ItemForInventoryStatement(Guid id, string sku, string name, MeasurementUnit unit)
        {
            Id = id;
            SKU = sku;
            Name = name;
            Unit = unit;
        }
        public Guid Id { get; private set; }
        public string SKU { get; private set; }
        public string Name { get; private set; }
        public MeasurementUnit Unit { get; private set; }        
    }
}