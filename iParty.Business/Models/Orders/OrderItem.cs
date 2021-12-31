using iParty.Business.Models.Items;

namespace iParty.Business.Models.Orders
{
    public class OrderItem: Entity
    {        
        public ItemForOrder Item  { get; set; }        
        public MeasurementUnit Unit { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }

        public decimal CalculateTotal()
        {
            return Quantity * Price;
        }
    }
}
