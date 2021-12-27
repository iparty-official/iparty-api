using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Messages;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Validations
{
    public class MessageValidation : AbstractValidator<Message>, IMessageValidation
    {        
        public MessageValidation(IRepository<Person> personRepository)
        {
            RuleFor(p => p.From).NotNull().WithMessage("O remetente da mensagem não foi informado.");
            
            RuleFor(p => p.To).NotNull().WithMessage("O destinatário da mensagem não foi informado.");
            
            RuleFor(p => p.From.Id == p.To.Id).Equal(false).WithMessage("O remetente e o destinatário da mensagem são iguais.");
            
            RuleFor(p => p.Text).NotEmpty().WithMessage("O texto da mensagem não foi informado.");
            
            RuleFor(p => p.DateTime).NotNull().WithMessage("A data/hora da mensagem não foi informada.");

            RuleFor(p => p.DateTime).GreaterThan(DateTime.MinValue).WithMessage("A data/hora da mensagem não foi informada.");

            RuleFor(x => personRepository.RecoverById(x.From.Id)).NotNull().WithMessage("O remetente informado não existe.");

            RuleFor(x => personRepository.RecoverById(x.To.Id)).NotNull().WithMessage("O destinatário informado não existe.");
        }
    }
}
