using iParty.Business.Models.InventoryStatements;
using System;

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
