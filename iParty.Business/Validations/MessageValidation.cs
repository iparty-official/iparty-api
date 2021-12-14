using FluentValidation;
using iParty.Business.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Validations
{
    public class MessageValidation : AbstractValidator<Message>
    {        
        public MessageValidation()
        {
            RuleFor(p => p.From).NotNull().WithMessage("O remetente da mensagem não foi informado.");
            RuleFor(p => p.To).NotNull().WithMessage("O destinatário da mensagem não foi informado.");
            RuleFor(p => p.From.Id == p.To.Id).Equal(false).WithMessage("O remetente e o destinatário da mensagem são iguais.");
            RuleFor(p => p.Text).NotEmpty().WithMessage("O texto da mensagem não foi informado.");
            RuleFor(p => p.Date).NotNull().WithMessage("A data da mensagem não foi informada.");
            RuleFor(p => p.Time).NotNull().WithMessage("A hora da mensagem não foi informada.");
        }
    }
}
