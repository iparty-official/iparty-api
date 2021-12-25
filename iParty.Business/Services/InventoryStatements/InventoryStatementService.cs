using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.InventoryStatements;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Services.InventoryStatements
{
    public class InventoryStatementService : Service<InventoryStatement, IRepository<InventoryStatement>>, IInventoryStatementService
    {
        private IInventoryStatementValidation _inventoryStatementValidation;

        public InventoryStatementService(IRepository<InventoryStatement> rep, IInventoryStatementValidation inventoryStatementValidation) : base(rep)
        {
            _inventoryStatementValidation = inventoryStatementValidation;
        }

        public ServiceResult<InventoryStatement> Create(InventoryStatement inventoryStatement)
        {
            var result = _inventoryStatementValidation.Validate(inventoryStatement);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(inventoryStatement);

            return GetSuccessResult(inventoryStatement);
        }

        public ServiceResult<InventoryStatement> Update(Guid id, InventoryStatement inventoryStatement)
        {
            var currentInventoryStatement = Get(id);

            if (currentInventoryStatement == null)
                return GetFailureResult("Não foi possível localizar o item informado.");

            var result = _inventoryStatementValidation.Validate(inventoryStatement);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, inventoryStatement);

            return GetSuccessResult(inventoryStatement);
        }
    }
}
