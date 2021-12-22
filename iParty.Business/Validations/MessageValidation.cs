using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Messages;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Validations
{
    public class MessageValidation : AbstractValidator<Message>, IMessageValidation
    {
        private bool personExists(Guid id, IRepository<Person> personRepository)
        {
            var person = personRepository.RecoverById(id);

            return person != null;
        }

        public MessageValidation(IRepository<Person> personRepository)
        {
            RuleFor(p => p.From).NotNull().WithMessage("O remetente da mensagem não foi informado.");
            
            RuleFor(p => p.To).NotNull().WithMessage("O destinatário da mensagem não foi informado.");
            
            RuleFor(p => p.From.Id == p.To.Id).Equal(false).WithMessage("O remetente e o destinatário da mensagem são iguais.");
            
            RuleFor(p => p.Text).NotEmpty().WithMessage("O texto da mensagem não foi informado.");
            
            RuleFor(p => p.DateTime).NotNull().WithMessage("A data/hora da mensagem não foi informada.");
            
            RuleFor(x => personExists(x.From.Id, personRepository)).Equal(true).WithMessage("O remetente informado não existe.");

            RuleFor(x => personExists(x.To.Id, personRepository)).Equal(true).WithMessage("O destinatário informado não existe.");
        }
    }
}
