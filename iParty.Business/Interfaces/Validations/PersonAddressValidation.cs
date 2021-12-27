using FluentValidation;
using iParty.Business.Models.People;

namespace iParty.Business.Interfaces.Validations
{
    public interface IPersonAddressValidation : IValidator<Person>
    {
    }
}
