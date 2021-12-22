using FluentValidation;
using iParty.Business.Models.Messages;

namespace iParty.Business.Interfaces.Validations
{
    public interface IMessageValidation : IValidator<Message>
    {
    }
}
