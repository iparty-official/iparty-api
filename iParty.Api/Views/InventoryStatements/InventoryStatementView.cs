using iParty.Api.Views.Items;
using iParty.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
