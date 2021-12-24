using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Items;

namespace iParty.Business.Validations
{
    public class ScheduleValidation : AbstractValidator<Schedule>, IScheduleValidation
    {
        public ScheduleValidation()
        {
            RuleFor(x => x.DayOfWeek).IsInEnum().WithMessage("O dia da semana informado é inválido.");

            RuleFor(x => x.Hours).NotEmpty().WithMessage("Nenhum horário foi informado.");            

            RuleForEach(x => x.Hours).Must(hour => hour.InitialHour < hour.FinalHour).WithMessage("A hora inicial precisa ser menor que a hora final");
        }
    }
}
