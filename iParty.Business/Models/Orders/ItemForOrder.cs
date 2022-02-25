using System;

namespace iParty.Business.Models.Orders
{
    public class ItemForOrder
    {
        public ItemForOrder() { }
        public ItemForOrder(Guid id, string sku, string name)
        {
            Id = id;
            SKU = sku;
            Name = name;
        }
        public Guid Id { get; private set; }
        public string SKU { get; private set; }
        public string Name { get; private set; }       
    }
}
