using FluentValidation;
using FluentValidation.Results;
using iParty.Business.Models.Items;

namespace iParty.Business.Interfaces.Validations
{
    public interface IItemValidation : IValidator<Item>
    {
        public ValidationResult CustomValidate(Item item);        
    }
}
