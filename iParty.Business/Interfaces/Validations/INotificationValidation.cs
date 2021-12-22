using FluentValidation;
using iParty.Business.Models.Notications;

namespace iParty.Business.Interfaces.Validations
{
    public interface INotificationValidation : IValidator<Notification>
    {
    }
}
