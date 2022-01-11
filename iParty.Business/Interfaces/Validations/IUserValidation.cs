using FluentValidation;
using iParty.Business.Models.Users;

namespace iParty.Business.Interfaces.Validations
{
    public interface IUserValidation : IValidator<User>
    {
    }
}
