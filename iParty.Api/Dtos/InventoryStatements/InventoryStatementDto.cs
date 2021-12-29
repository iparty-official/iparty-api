using iParty.Business;
using iParty.Business.Models.InventoryStatements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParty.Api.Dtos.InventoryStatements
{
    public class InventoryStatementDto
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public InOrOut InOrOut { get; set; }
        public DateTime DataTime { get; set; }
    }
}
