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

            RuleFor(x => x.Prefix).NotEmpty().WithMessage("O prefixo do número do telefone não foi informado.");

            RuleFor(x => x.Prefix).Length(2).WithMessage("O prefixo do número do telefone deve conter exatamentee dois dígitos.");

            RuleFor(x => x.Number).NotEmpty().WithMessage("O número do telefone não foi informado.");

            RuleFor(x => x.Number).Length(8, 11).WithMessage("O número do telefone deve conter entre oito e onze dígitos.");

            RuleFor(x => true).Equal(x => x.Prefix.All(char.IsDigit)).WithMessage("O prefixo do telefone deve conter apenas números.");

            RuleFor(x => true).Equal(x => x.Number.All(char.IsDigit)).WithMessage("O número do telefone deve conter apenas números.");          
        }        
    }
}