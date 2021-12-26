using FluentValidation;
using iParty.Business.Models.Items;

namespace iParty.Business.Interfaces.Validations
{
    public interface IScheduleValidation : IValidator<Schedule>
    {
    }
}
