using FluentValidation;
using iParty.Business.Models.Notications;

namespace iParty.Business.Validations
{
    public class NotificationValidation : AbstractValidator<Notification>
    {
        public NotificationValidation()
        {
            RuleFor(p => p.Destination).NotNull().WithMessage("O destinatário da notificação não foi informado.");                        
            RuleFor(p => p.Text).NotEmpty().WithMessage("O texto da notificação não foi informado.");
            RuleFor(p => p.Date).NotNull().WithMessage("A data da notificação não foi informada.");
            RuleFor(p => p.Time).NotNull().WithMessage("O tempo da notificação não foi informado.");
        }
    }
}
