using iParty.Business.Infra;
using iParty.Business.Models.InventoryStatements;
using System;

namespace iParty.Business.Interfaces.Services
{
    public interface IInventoryStatementService : IService<InventoryStatement>
    {
        public ServiceResult<InventoryStatement> Create(InventoryStatement inventoryStatement);     
    }
}
