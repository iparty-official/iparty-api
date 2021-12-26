using FluentValidation;
using iParty.Business.Models.Items;

namespace iParty.Business.Interfaces.Validations
{
    public interface IItemScheduleValidation : IValidator<Item>
    {
    }
}
