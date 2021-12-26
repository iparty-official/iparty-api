﻿using iParty.Api.Dtos.InventoryStatements;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.InventoryStatements;
using iParty.Api.Views.Items;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.InventoryStatements;
using iParty.Business.Models.Items;
using iParty.Data.Repositories;
using System.Collections.Generic;

namespace iParty.Api.Mappers.InventoryStatements
{
    public class InventoryStatementMapper : BaseMapper<InventoryStatement>, IInventoryStatementMapper
    {        
        private IRepository<Item> _itemRepository;

        public InventoryStatementMapper(IRepository<Item> itemRepository)
        {            
            _itemRepository = itemRepository;
        }

        public MapperResult<InventoryStatement> Map(InventoryStatementDto dto)
        {
            var product = _itemRepository.RecoverById(dto.ProductId).IfNull(() => { AddError("O item informado não existe."); });

            if (!SuccessResult()) return GetResult();

            var inventoryStatement = new InventoryStatement()
            {
                DateTime = dto.DataTime,
                InOrOut = dto.InOrOut,
                Product = product,
                Quantity = dto.Quantity                
            };            

            SetEntity(inventoryStatement);

            return GetResult();
        }

        public InventoryStatementView Map(InventoryStatement inventoryStatement)
        {
            return mapEntityToView(inventoryStatement);
        }

        public List<InventoryStatementView> Map(List<InventoryStatement> inventoryStatements)
        {
            var result = new List<InventoryStatementView>();

            foreach (var inventoryStatement in inventoryStatements)
            {
                result.Add(mapEntityToView(inventoryStatement));
            }

            return result;
        }

        private InventoryStatementView mapEntityToView(InventoryStatement inventoryStatement)
        {
            if (inventoryStatement == null) return null;

            var product = new ItemSummarizedView() { Id = inventoryStatement.Product.Id, Name = inventoryStatement.Product.Name };

            var inventoryStatementView = new InventoryStatementView()
            {
                Id = inventoryStatement.Id,
                DataTime = inventoryStatement.DateTime,
                InOrOut = inventoryStatement.InOrOut,
                Product = product,
                Quantity = inventoryStatement.Quantity
            };            

            return inventoryStatementView;
        }

    }
}