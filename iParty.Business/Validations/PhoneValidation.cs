using FluentValidation;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System.Linq;

namespace iParty.Business.Validations
{
    public class PhoneValidation : AbstractValidator<Phone>
    {        
        public PhoneValidation()
        {
            //TODO: Impedir telefones repetidos
            //TODO: Validar quantidade de dígitos do telefone

            RuleFor(x => x.Prefix).NotEmpty().WithMessage("O prefixo do número do telefone não foi informado.");

            RuleFor(x => x.Number).NotEmpty().WithMessage("O número do telefone não foi informado.");

            RuleFor(x => true).Equal(x => x.Prefix.All(char.IsDigit)).WithMessage("O prefixo do telefone deve conter apenas números.");

            RuleFor(x => true).Equal(x => x.Number.All(char.IsDigit)).WithMessage("O número do telefone deve conter apenas números.");
        }        
    }
}