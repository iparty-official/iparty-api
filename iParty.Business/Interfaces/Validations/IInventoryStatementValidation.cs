using FluentValidation;
using iParty.Business.Models.InventoryStatements;

namespace iParty.Business.Interfaces.Validations
{
    public interface IInventoryStatementValidation : IValidator<InventoryStatement>
    {        
    }
}
