using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Validations
{
    public class MessageValidation : AbstractValidator<Message>, IMessageValidation
    {        
        public MessageValidation()
        {
            RuleFor(p => p.From).NotNull().WithMessage("O remetente da mensagem não foi informado.");
            RuleFor(p => p.To).NotNull().WithMessage("O destinatário da mensagem não foi informado.");
            RuleFor(p => p.From.Id == p.To.Id).Equal(false).WithMessage("O remetente e o destinatário da mensagem são iguais.");
            RuleFor(p => p.Text).NotEmpty().WithMessage("O texto da mensagem não foi informado.");
            RuleFor(p => p.DateTime).NotNull().WithMessage("A data/hora da mensagem não foi informada.");            
        }
    }
}
