using iParty.Api.Views.Items;
using iParty.Business.Models.InventoryStatements;
using System;

namespace iParty.Api.Views.InventoryStatements
{
    public class InventoryStatementView : View
    {
        public ItemSummarizedView Product { get; set; }
        public decimal Quantity { get; set; }
        public InOrOut InOrOut { get; set; }
        public DateTime DataTime { get; set; }
    }
}
