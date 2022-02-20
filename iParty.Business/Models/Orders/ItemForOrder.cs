namespace iParty.Business.Models.Orders
{
    public class ItemForOrder: Entity
    {
        public ItemForOrder() { }
        public ItemForOrder(string sKU, string name)
        {
            SKU = sKU;
            Name = name;
        }
        public string SKU { get; private set; }
        public string Name { get; private set; }       
    }
}
