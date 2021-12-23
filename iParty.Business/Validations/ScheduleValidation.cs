using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Items;

namespace iParty.Business.Validations
{
    public class ScheduleValidation : AbstractValidator<Schedule>, IScheduleValidation
    {
        public ScheduleValidation()
        {
            //TODO: Implementar validação da agenda
        }
    }
}
