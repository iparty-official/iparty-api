using iParty.Business.Models.Items;
using System;

namespace iParty.Business.Models.InventoryStatements
{
    public class InventoryStatement: Entity
    {
        public InventoryStatement() : base() { }
        public InventoryStatement(ItemForInventoryStatement product, decimal quantity, InOrOut inOrOut, DateTime dateTime)
        {
            Product = product;
            Quantity = quantity;
            InOrOut = inOrOut;
            DateTime = dateTime;
        }
        public ItemForInventoryStatement Product { get; private set; }
        public decimal Quantity { get; private set; }
        public InOrOut InOrOut { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
