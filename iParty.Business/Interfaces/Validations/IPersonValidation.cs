using FluentValidation;
using FluentValidation.Results;
using iParty.Business.Models.People;

namespace iParty.Business.Interfaces.Validations
{
    public interface IPersonValidation : IValidator<Person>
    {
        public ValidationResult CustomValidate(Person person);
    }
}
