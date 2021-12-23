using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Items;

namespace iParty.Business.Validations
{
    public class ItemValidation : AbstractValidator<Item>, IItemValidation
    {
        public ItemValidation()
        {
        }
    }
}
