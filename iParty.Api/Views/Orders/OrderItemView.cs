using iParty.Api.Views.Items;

namespace iParty.Api.Views.Orders
{
    public class OrderItemView : View
    {
        public ItemSummarizedView Item { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
