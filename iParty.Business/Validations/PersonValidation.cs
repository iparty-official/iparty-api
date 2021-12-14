using FluentValidation;
using iParty.Business.Models.People;

namespace iParty.Business.Validations
{
    public class PersonValidation : AbstractValidator<Person>
    {
        public PersonValidation()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Nome não foi informado.");
            RuleFor(p => p.Document).NotEmpty().WithMessage("Documento não informado.");                    
        }
    }
}
