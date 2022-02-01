using AutoMapper;
using iParty.Api.Dtos.InventoryStatements;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.InventoryStatements;
using iParty.Api.Views.Items;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.InventoryStatements;
using iParty.Business.Models.Items;
using iParty.Business.Interfaces;
using System.Collections.Generic;

namespace iParty.Api.Mappers.InventoryStatements
{
    public class InventoryStatementMapper : BaseMapper<InventoryStatement>, IInventoryStatementMapper
    {        
        private IRepository<Item> _itemRepository;

        private IMapper _autoMapper;

        public InventoryStatementMapper(IRepository<Item> itemRepository, IMapper autoMapper)
        {            
            _itemRepository = itemRepository;
            _autoMapper = autoMapper;
        }

        public MapperResult<InventoryStatement> Map(InventoryStatementDto dto)
        {
            var product = _itemRepository.RecoverById(dto.ProductId).IfNull(() => { AddError("O item informado não existe."); });

            if (!SuccessResult()) return GetResult();            

            SetEntity(new InventoryStatement(product, dto.Quantity, dto.InOrOut, dto.DataTime));

            return GetResult();
        }

        public InventoryStatementView Map(InventoryStatement inventoryStatement)
        {
            return mapToView(inventoryStatement);
        }

        public List<InventoryStatementView> Map(List<InventoryStatement> inventoryStatements)
        {
            var result = new List<InventoryStatementView>();

            foreach (var inventoryStatement in inventoryStatements)
            {
                result.Add(mapToView(inventoryStatement));
            }

            return result;
        }

        private InventoryStatementView mapToView(InventoryStatement inventoryStatement)
        {
            if (inventoryStatement == null) return null;

            var product = _autoMapper.Map<ItemSummarizedView>(inventoryStatement.Product);

            var inventoryStatementView = new InventoryStatementView()
            {
                Id = inventoryStatement.Id,
                Version = inventoryStatement.Version,
                DataTime = inventoryStatement.DateTime,
                InOrOut = inventoryStatement.InOrOut,
                Product = product,
                Quantity = inventoryStatement.Quantity
            };            

            return inventoryStatementView;
        }

    }
}
