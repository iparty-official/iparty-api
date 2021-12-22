using FluentValidation;
using iParty.Business.Models.Addresses;

namespace iParty.Business.Interfaces.Validations
{
    public interface IAddressValidation : IValidator<Address>
    {
    }
}
