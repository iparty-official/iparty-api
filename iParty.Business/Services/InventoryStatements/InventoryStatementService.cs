﻿using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.InventoryStatements;
using iParty.Business.Models.Items;
using iParty.Business.Interfaces;
using System;
using System.Collections.Generic;

namespace iParty.Business.Services.InventoryStatements
{
    public class InventoryStatementService : IInventoryStatementService
    {
        private BasicService<InventoryStatement> _basicService;

        private IItemService _itemService;

        private IRepository<InventoryStatement> _repository;

        private IInventoryStatementValidation _inventoryStatementValidation;

        public InventoryStatementService(IRepository<InventoryStatement> repository, IInventoryStatementValidation inventoryStatementValidation, IItemService itemService)
        {            
            _itemService = itemService;

            _basicService = new BasicService<InventoryStatement>(repository, inventoryStatementValidation);

            _inventoryStatementValidation = inventoryStatementValidation;

            _repository = repository;
        }

        public ServiceResult<InventoryStatement> Create(InventoryStatement inventoryStatement)
        {            
            var result = _inventoryStatementValidation.Validate(inventoryStatement);

            if (!result.IsValid)
                return ServiceResult<InventoryStatement>.FailureResult(result);

            ServiceResult<Item> itemUpdateResult;

            if (inventoryStatement.InOrOut == InOrOut.In)
                itemUpdateResult = _itemService.IncreaseAvailableQuantity(inventoryStatement.Product.Id, inventoryStatement.Quantity);
            else
                itemUpdateResult = _itemService.DecreaseAvailableQuantity(inventoryStatement.Product.Id, inventoryStatement.Quantity);

            if (!itemUpdateResult.Success)
                return ServiceResult<InventoryStatement>.FailureResult(itemUpdateResult.Errors);

            _repository.Create(inventoryStatement);

            return ServiceResult<InventoryStatement>.SuccessResult(inventoryStatement);
        }

        public ServiceResult<InventoryStatement> Delete(Guid id)
        {
            var inventoryStatement = Get(id);

            if (inventoryStatement == null)
                return ServiceResult<InventoryStatement>.FailureResult("Não foi possível localizar o registro informado.");

            ServiceResult<Item> itemUpdateResult;

            if (inventoryStatement.InOrOut == InOrOut.In)
                itemUpdateResult = _itemService.DecreaseAvailableQuantity(inventoryStatement.Product.Id, inventoryStatement.Quantity);
            else
                itemUpdateResult = _itemService.IncreaseAvailableQuantity(inventoryStatement.Product.Id, inventoryStatement.Quantity);

            if (!itemUpdateResult.Success)
                return ServiceResult<InventoryStatement>.FailureResult(itemUpdateResult.Errors);

            _repository.Delete(id);

            return ServiceResult<InventoryStatement>.SuccessResult(inventoryStatement);                                    
        }

        public InventoryStatement Get(Guid id)
        {
            return _basicService.Get(id);
        }

        public List<InventoryStatement> Get()
        {
            return _basicService.Get();
        }
    }
}
