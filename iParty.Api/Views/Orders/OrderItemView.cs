using iParty.Api.Views.Items;
using iParty.Business.Models.Items;

namespace iParty.Api.Views.Orders
{
    public class OrderItemView : View
    {
        public ItemSummarizedView Item { get; set; }        
        public MeasurementUnit Unit { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
