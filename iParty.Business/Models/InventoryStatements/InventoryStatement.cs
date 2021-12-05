using iParty.Business.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.InventoryStatements
{
    public class InventoryStatement: Entity
    {        
        public Item Product { get; set; }
        public decimal Quantity { get; set; }
        public InOrOut InOrOut { get; set; }
        public DateTime Data { get; set; }
        public DateTime Time{ get; set; }
    }
}
