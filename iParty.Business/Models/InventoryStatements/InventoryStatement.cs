using iParty.Business.Models.Items;
using System;

namespace iParty.Business.Models.InventoryStatements
{
    public class InventoryStatement: Entity
    {        
        public Item Product { get; set; }
        public decimal Quantity { get; set; }
        public InOrOut InOrOut { get; set; }
        public DateTime DateTime { get; set; }
    }
}
