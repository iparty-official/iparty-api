using iParty.Api.Dtos.InventoryStatements;
using iParty.Api.Infra;
using iParty.Api.Views.InventoryStatements;
using iParty.Business.Models.InventoryStatements;
using System.Collections.Generic;

namespace iParty.Api.Interfaces.Mappers
{
    public interface IInventoryStatementMapper
    {
        MapperResult<InventoryStatement> Map(InventoryStatementDto dto);

        InventoryStatementView Map(InventoryStatement entity);

        List<InventoryStatementView> Map(List<InventoryStatement> entities);
    }
}
