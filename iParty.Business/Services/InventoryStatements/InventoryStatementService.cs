using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.InventoryStatements;
using iParty.Business.Models.Items;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Services.InventoryStatements
{
    public class InventoryStatementService : Service<InventoryStatement, IRepository<InventoryStatement>>, IInventoryStatementService
    {
        private IInventoryStatementValidation _inventoryStatementValidation;

        private IItemService _itemService;

        public InventoryStatementService(IRepository<InventoryStatement> rep, IInventoryStatementValidation inventoryStatementValidation, IItemService itemService) : base(rep)
        {
            _inventoryStatementValidation = inventoryStatementValidation;
            _itemService = itemService;
        }

        public ServiceResult<InventoryStatement> Create(InventoryStatement inventoryStatement)
        {
            //TODO: Essa rotina tem duas operações: Atualizar o estoque do item e lançar um movimento.
            //Elas não estão protegida por uma transação atômica.
            //Implementar mecanismo que garanta a execução de ambas operações ou que garanta o desfazimento de ambas

            var result = _inventoryStatementValidation.Validate(inventoryStatement);

            if (!result.IsValid)
                return GetFailureResult(result);

            ServiceResult<Item> itemUpdateResult;

            if (inventoryStatement.InOrOut == InOrOut.In)
                itemUpdateResult = _itemService.IncreaseAvailableQuantity(inventoryStatement.Product.Id, inventoryStatement.Quantity);
            else
                itemUpdateResult = _itemService.DecreaseAvailableQuantity(inventoryStatement.Product.Id, inventoryStatement.Quantity);

            if (!itemUpdateResult.Success)
                return new ServiceResult<InventoryStatement>() { Success = false, Errors = itemUpdateResult.Errors };

            Rep.Create(inventoryStatement);

            return GetSuccessResult(inventoryStatement);
        }

        public override ServiceResult<InventoryStatement> Delete(Guid id)
        {
            var inventoryStatement = Get(id);

            if (inventoryStatement == null)
                return GetFailureResult("Não foi possível localizar o registro informado.");

            ServiceResult<Item> itemUpdateResult;

            if (inventoryStatement.InOrOut == InOrOut.In)
                itemUpdateResult = _itemService.DecreaseAvailableQuantity(inventoryStatement.Product.Id, inventoryStatement.Quantity);
            else
                itemUpdateResult = _itemService.IncreaseAvailableQuantity(inventoryStatement.Product.Id, inventoryStatement.Quantity);

            if (!itemUpdateResult.Success)
                return new ServiceResult<InventoryStatement>() { Success = false, Errors = itemUpdateResult.Errors };

            Rep.Delete(id);

            return GetSuccessResult(inventoryStatement);                                    
        }
    }
}
