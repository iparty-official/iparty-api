using FluentValidation;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.Cities;

namespace iParty.Business.Interfaces.Validations
{
    public interface ICityValidation : IValidator<City>
    {
    }
}
